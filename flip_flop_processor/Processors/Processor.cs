using System;
using System.Threading.Tasks;
using flip_flop_core;
using flip_flop_core.Enums;
using flip_flop_core.Messages;
using flip_flop_dal.Enitites;
using flip_flop_dal.Workers;
using flip_flop_game_logic.Factories;
using flip_flop_game_logic.Logic;
using flip_flop_processor.Factories;
using flip_flop_processor.Models;
using flip_flop_services.Casino;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace flip_flop_processor.Processors
{
    public class Processor : IProcessor
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICasinoService _casinoService;
        private readonly IAuthProcessorResponseFactory _authProcessorResponseFactory;
        private readonly IFlipProcessorResponseFactory _flipProcessorResponseFactory;
        private readonly IErrorResponseFactory _errorResponseFactory;
        private readonly IGameCalculator _gameCalculator;
        private readonly ILogger _logger;

        public Processor(IUnitOfWork unitOfWork, ICasinoService casinoService, IAuthProcessorResponseFactory authProcessorResponseFactory,
            IErrorResponseFactory errorResponseFactory, ILogger<Processor> logger, IGameCalculator gameCalculator, IFlipProcessorResponseFactory flipProcessorResponseFactory)
        {
            _unitOfWork = unitOfWork;
            _casinoService = casinoService;
            _authProcessorResponseFactory = authProcessorResponseFactory;
            _errorResponseFactory = errorResponseFactory;
            _flipProcessorResponseFactory = flipProcessorResponseFactory;
            _gameCalculator = gameCalculator;
            _logger = logger;
        }

        public IAuthProcessorResponse ProcessAuth(IAuthProcessor authProcessorModel)
        {
            _logger.LogInformation(string.Format(Messages.Logger_Started_Processing, WebCallName.Authentication, JsonConvert.SerializeObject(authProcessorModel)));

            IAuthProcessorResponse authProcessorResponse = null;

            try
            {
                var casinoAuthResponse = _casinoService.Auth(authProcessorModel.GameId, authProcessorModel.Token,authProcessorModel.Currency);

                if (casinoAuthResponse.HasError)
                {
                    _logger.LogWarning(Messages.Logger_Casino_Error, CasinoCallName.Authentication, JsonConvert.SerializeObject(casinoAuthResponse));
                    authProcessorResponse = _authProcessorResponseFactory.Create(-1, _errorResponseFactory.Create((int)ErrorCode.Authentication, string.Format(Messages.Logger_Generic_Error, WebCallName.Authentication)));
                }
                else
                {
                    var balanceResponse = _casinoService.GetBalance(authProcessorModel.Token, casinoAuthResponse.PlayerId.Value, authProcessorModel.Currency);

                    if (balanceResponse.HasError)
                    {
                        _logger.LogWarning(Messages.Logger_Casino_Error, CasinoCallName.GetBalance, JsonConvert.SerializeObject(balanceResponse));
                        authProcessorResponse = _authProcessorResponseFactory.Create(-1, _errorResponseFactory.Create((int)ErrorCode.GetBalance, string.Format(Messages.Logger_Generic_Error, WebCallName.Authentication)));
                    }
                    else
                    {
                        var player = _unitOfWork.PlayerRepository.FetchByPrimaryKey(casinoAuthResponse.PlayerId);

                        if (player == null)
                        {
                            _unitOfWork.PlayerRepository.Add(new Player
                            {
                                PlayerId = casinoAuthResponse.PlayerId.Value,
                                ExternalId = casinoAuthResponse.ExternalId.Value,
                                PartnerId = casinoAuthResponse.PartnerId.Value,
                                Username = casinoAuthResponse.UserName
                            });

                            _unitOfWork.Save();
                        }

                        authProcessorResponse = _authProcessorResponseFactory.Create(balanceResponse.TotalBalance.Value, _errorResponseFactory.Create());
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.Logger_Generic_Error, WebCallName.Authentication);
                authProcessorResponse = _authProcessorResponseFactory.Create(-1, _errorResponseFactory.Create((int)ErrorCode.Generic, string.Format(Messages.Logger_Generic_Error, WebCallName.Authentication)));
            }

            _logger.LogInformation(string.Format(Messages.Logger_Ended_Processing, WebCallName.Authentication, JsonConvert.SerializeObject(authProcessorResponse)));

            return authProcessorResponse;
        }

        public IFlipProcessorResponse ProcessFlip(IFlipProcessor flipProcessorModel)
        {
            _logger.LogInformation(string.Format(Messages.Logger_Started_Processing, WebCallName.Flip, JsonConvert.SerializeObject(flipProcessorModel)));

            decimal winAmount = 0;
            bool isSuccedded = false;

            IFlipProcessorResponse flipProcessorResponse = null;

            try
            {
                var casinoBalanceResponse = _casinoService.GetBalance(flipProcessorModel.Token, flipProcessorModel.PlayerId, flipProcessorModel.Currency);

                if (!casinoBalanceResponse.HasError)
                {
                    var casinoDoBetResponse = _casinoService.DoBet(flipProcessorModel.BetAmount, flipProcessorModel.TransactionId.ToString(), flipProcessorModel.GameId, flipProcessorModel.Token, flipProcessorModel.PlayerId, flipProcessorModel.Currency);

                    if (!casinoDoBetResponse.HasError)
                    {
                        var calculateRandomWin = _gameCalculator.CalculateRandomWin(flipProcessorModel.BetAmount);

                        if (calculateRandomWin.IsWin)
                        {
                            var casinoWinResponse = _casinoService.DoWin(calculateRandomWin.WinAmount, flipProcessorModel.TransactionId.ToString(), flipProcessorModel.GameId, flipProcessorModel.Token, flipProcessorModel.PlayerId, flipProcessorModel.Currency);

                            winAmount = calculateRandomWin.WinAmount;

                            if (!casinoWinResponse.HasError)
                            {
                                flipProcessorResponse = _flipProcessorResponseFactory.Create(calculateRandomWin.WinAmount, casinoWinResponse.Balance.Value, _errorResponseFactory.Create());
                                isSuccedded = true;
                            }
                            else
                            {
                                _logger.LogWarning(Messages.Logger_Casino_Error, CasinoCallName.DoWin, JsonConvert.SerializeObject(casinoWinResponse));

                                var doRollebackResponse = _casinoService.DoRolleback(flipProcessorModel.TransactionId.ToString(), flipProcessorModel.GameId, flipProcessorModel.Token, flipProcessorModel.PlayerId, flipProcessorModel.Currency);

                                if (!doRollebackResponse.HasError)
                                {
                                    flipProcessorResponse = _flipProcessorResponseFactory.Create(-1, -1, _errorResponseFactory.Create((int)ErrorCode.DoWin, string.Format(Messages.Logger_Generic_Error, WebCallName.Flip)));
                                }
                                else
                                {
                                    _logger.LogWarning(Messages.Logger_Casino_Error, CasinoCallName.DoRolleback, JsonConvert.SerializeObject(doRollebackResponse));
                                    flipProcessorResponse = _flipProcessorResponseFactory.Create(-1, -1, _errorResponseFactory.Create((int)ErrorCode.DoRolleback, string.Format(Messages.Logger_Generic_Error, WebCallName.Flip)));
                                }
                            }
                        }
                        else
                        {
                            flipProcessorResponse = _flipProcessorResponseFactory.Create(0, casinoDoBetResponse.Balance.Value, _errorResponseFactory.Create());
                            isSuccedded = true;
                        }
                    }
                    else
                    {
                        _logger.LogWarning(Messages.Logger_Casino_Error, CasinoCallName.DoBet, JsonConvert.SerializeObject(casinoDoBetResponse));
                        flipProcessorResponse = _flipProcessorResponseFactory.Create(-1, -1, _errorResponseFactory.Create((int)ErrorCode.DoBet, string.Format(Messages.Logger_Generic_Error, WebCallName.Flip)));
                    }
                }
                else
                {
                    _logger.LogWarning(Messages.Logger_Casino_Error, CasinoCallName.GetBalance, JsonConvert.SerializeObject(casinoBalanceResponse));
                    flipProcessorResponse = _flipProcessorResponseFactory.Create(-1, -1, _errorResponseFactory.Create((int)ErrorCode.GetBalance, string.Format(Messages.Logger_Generic_Error, WebCallName.Flip)));
                }

                _unitOfWork.TransactionRepository.Add(new Transaction
                {
                    BetAmount = flipProcessorModel.BetAmount,
                    Currency = flipProcessorModel.Currency,
                    GameId = flipProcessorModel.GameId,
                    GeneratedTransactionId = flipProcessorModel.TransactionId,
                    IsSucceeded = isSuccedded,
                    PlayerId = flipProcessorModel.PlayerId,
                    TransactionDateTime = DateTime.Now,
                    WinAmount = winAmount
                });

                _unitOfWork.Save();

            }
            catch (Exception ex)
            {
                flipProcessorResponse = _flipProcessorResponseFactory.Create(-1, -1, _errorResponseFactory.Create((int)ErrorCode.Generic, string.Format(Messages.Logger_Generic_Error, WebCallName.Flip)));
                _logger.LogError(ex, Messages.Logger_Generic_Error, WebCallName.Flip);
            }

            _logger.LogInformation(string.Format(Messages.Logger_Ended_Processing, WebCallName.Flip, JsonConvert.SerializeObject(flipProcessorResponse)));

            return flipProcessorResponse;
        }
    }
}
