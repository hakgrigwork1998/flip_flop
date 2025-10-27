using flip_flop_core.Provider;
using flip_flop_dal.Workers;
using flip_flop_processor.Factories;
using flip_flop_processor.Processors;
using flip_flop_services.Casino;
using Moq;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using flip_flop_game_logic.Logic;
using flip_flop_game_logic.Factories;
using flip_flop_core.Enums;
using flip_flop_services.Factories;
using flip_flop_dal.Enitites;
using flip_flop_core.Messages;
using flip_flop_game_logic.Models;

namespace flip_flop_tests.Tests
{
    [TestFixture]
    public class ProcessorTests
    {
        private IAuthResponseFactory _authResponseFactory;
        private IGetBalanceResponseFactory _getBalanceResponseFactory;
        private IDoRollebackResponseFactory _doRollebackResponseFactory;
        private IDoWinResponseFactory _doWinResponseFactory;
        private IDoBetResponseFactory _doBetResponseFactory;
        private IAuthProcessorFactory _authProcessorFactory;
        private IFlipProcessorFactory _flipProcessorFactory;
        private IFlipDataResponseFactory _flipDataResponseFactory;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _authResponseFactory = SingletonProvider.ServiceProvider.GetService<IAuthResponseFactory>();
            _getBalanceResponseFactory = SingletonProvider.ServiceProvider.GetService<IGetBalanceResponseFactory>();
            _doRollebackResponseFactory = SingletonProvider.ServiceProvider.GetService<IDoRollebackResponseFactory>();
            _doWinResponseFactory = SingletonProvider.ServiceProvider.GetService<IDoWinResponseFactory>();
            _doBetResponseFactory = SingletonProvider.ServiceProvider.GetService<IDoBetResponseFactory>();
            _authProcessorFactory = SingletonProvider.ServiceProvider.GetService<IAuthProcessorFactory>();
            _flipProcessorFactory = SingletonProvider.ServiceProvider.GetService<IFlipProcessorFactory>();
            _flipDataResponseFactory = SingletonProvider.ServiceProvider.GetService<IFlipDataResponseFactory>();
        }

        [TestCase(388, "4357855ff4b58a4710fd050cdbd3e", "USD")]
        [TestCase(388, "4357855ff4b58a4710fd050cdbd3e", "AMD")]
        public void Processor_Auth_Success(int gameId, string token, string currency)
        {
            var mockService = new Mock<ICasinoService>();
            mockService
                .Setup((service) => service.Auth(gameId, token, currency))
                .Returns(_authResponseFactory.Create(currency, 1, 1, 1, "Test", true, token, false, 0, "", null));
            mockService
                .Setup((service) => service.GetBalance(token, 1, currency))
                .Returns(_getBalanceResponseFactory.Create("5000", 5000, currency, token, false, 0, "", null));

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup((unitOfWork) => unitOfWork.PlayerRepository.Add(new Player()));


            var processor = new Processor(mockUnitOfWork.Object, mockService.Object,
                SingletonProvider.ServiceProvider.GetService<IAuthProcessorResponseFactory>(),
                SingletonProvider.ServiceProvider.GetService<IErrorResponseFactory>(), new Mock<ILogger<Processor>>().Object,
                new Mock<IGameCalculator>().Object, SingletonProvider.ServiceProvider.GetService<IFlipProcessorResponseFactory>());


            var processAuthResponse = processor.ProcessAuth(_authProcessorFactory.Create(gameId, token, currency));

            Assert.That(processAuthResponse.ErrorResponse.ErrorCode == (int)ErrorCode.Success);
            Assert.That(processAuthResponse.ErrorResponse.Message == "Ok");
            Assert.That(processAuthResponse.Balance == 5000);
        }

        [TestCase(388, "4357855ff4b58a4710fd050cdbd3e", "USD")]
        [TestCase(388, "4357855ff4b58a4710fd050cdbd3e", "EUR")]
        public void Processor_Auth_Casino_Auth_Has_Error(int gameId, string token, string currency)
        {
            var mockService = new Mock<ICasinoService>();
            mockService
                .Setup((service) => service.Auth(gameId, token, currency))
                .Returns(_authResponseFactory.Create(null, null, null, null, null, true, null, true, 146, "", null));


            var processor = new Processor(new Mock<IUnitOfWork>().Object, mockService.Object,
               SingletonProvider.ServiceProvider.GetService<IAuthProcessorResponseFactory>(),
               SingletonProvider.ServiceProvider.GetService<IErrorResponseFactory>(), new Mock<ILogger<Processor>>().Object,
               new Mock<IGameCalculator>().Object, SingletonProvider.ServiceProvider.GetService<IFlipProcessorResponseFactory>());


            var processAuthResponse = processor.ProcessAuth(_authProcessorFactory.Create(gameId, token, currency));

            Assert.That(processAuthResponse.ErrorResponse.ErrorCode == (int)ErrorCode.Authentication);
            Assert.That(processAuthResponse.ErrorResponse.Message == string.Format(Messages.Logger_Generic_Error, WebCallName.Authentication));
            Assert.That(processAuthResponse.Balance == -1);
        }

        [TestCase(388, "4357855ff4b58a4710fd050cdbd3e", "USD")]
        [TestCase(388, "4357855ff4b58a4710fd050cdbd3e", "AMD")]
        public void Processor_Auth_Casino_Auth_Get_Balance_Has_Error(int gameId, string token, string currency)
        {
            var mockService = new Mock<ICasinoService>();
            mockService
                .Setup((service) => service.Auth(gameId, token, currency))
                .Returns(_authResponseFactory.Create(currency, 1, 1, 1, "Test", true, token, false, 0, "", null));
            mockService
                .Setup((service) => service.GetBalance(token, 1, currency))
                .Returns(_getBalanceResponseFactory.Create(null, null, null, null, true, 146, "", null));

            var processor = new Processor(new Mock<IUnitOfWork>().Object, mockService.Object,
            SingletonProvider.ServiceProvider.GetService<IAuthProcessorResponseFactory>(),
            SingletonProvider.ServiceProvider.GetService<IErrorResponseFactory>(), new Mock<ILogger<Processor>>().Object,
            new Mock<IGameCalculator>().Object, SingletonProvider.ServiceProvider.GetService<IFlipProcessorResponseFactory>());


            var processAuthResponse = processor.ProcessAuth(_authProcessorFactory.Create(gameId, token, currency));

            Assert.That(processAuthResponse.ErrorResponse.ErrorCode == (int)ErrorCode.GetBalance);
            Assert.That(processAuthResponse.ErrorResponse.Message == string.Format(Messages.Logger_Generic_Error, WebCallName.Authentication));
            Assert.That(processAuthResponse.Balance == -1);
        }

        [TestCase(388, "4357855ff4b58a4710fd050cdbd3e", "USD")]
        [TestCase(388, "4357855ff4b58a4710fd050cdbd3e", "AMD")]
        public void Processor_Auth_Casino_Auth_Throws_Exceptoin(int gameId, string token, string currency)
        {
            var mockService = new Mock<ICasinoService>();
            mockService
                .Setup((service) => service.Auth(gameId, token, currency))
                .Returns(_authResponseFactory.Create(currency, 1, 1, 1, "Test", true, token, false, 0, "", null));

            mockService
                .Setup((service) => service.GetBalance(token, 1, currency))
                .Returns(_getBalanceResponseFactory.Create("5000", 5000, currency, token, false, 0, "", null));

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup((unitOfWork) => unitOfWork.PlayerRepository.FetchByPrimaryKey(1))
                .Returns(() => null);
            mockUnitOfWork
                .Setup((unitOfWork) => unitOfWork.Save())
                .Throws(new Exception("Hello From Exception"));


            var processor = new Processor(mockUnitOfWork.Object, mockService.Object,
           SingletonProvider.ServiceProvider.GetService<IAuthProcessorResponseFactory>(),
           SingletonProvider.ServiceProvider.GetService<IErrorResponseFactory>(), new Mock<ILogger<Processor>>().Object,
           new Mock<IGameCalculator>().Object, SingletonProvider.ServiceProvider.GetService<IFlipProcessorResponseFactory>());


            var processAuthResponse = processor.ProcessAuth(_authProcessorFactory.Create(gameId, token, currency));

            Assert.That(processAuthResponse.ErrorResponse.ErrorCode == (int)ErrorCode.Generic);
            Assert.That(processAuthResponse.ErrorResponse.Message == string.Format(Messages.Logger_Generic_Error, WebCallName.Authentication));
            Assert.That(processAuthResponse.Balance == -1);
        }

        [TestCase(-1837, 388, "4357855ff4b58a4710fd050cdbd3e", 1, 2, 5000, true, "USD")]
        [TestCase(-1837, 388, "4357855ff4b58a4710fd050cdbd3e", 2, 4, 8000, true, "AMD")]
        [TestCase(-1837, 388, "4357855ff4b58a4710fd050cdbd3e", 3, 6, 1000, true, "EUR")]
        [TestCase(-1837, 388, "4357855ff4b58a4710fd050cdbd3e", 1, 0, 5000, false, "USD")]
        [TestCase(-1837, 388, "4357855ff4b58a4710fd050cdbd3e", 2, 0, 8000, false, "AMD")]
        [TestCase(-1837, 388, "4357855ff4b58a4710fd050cdbd3e", 3, 0, 1000, false, "AMD")]
        public void Processor_Flip_Success(int playerId, int gameId, string token, decimal betAmount, decimal winAmount, decimal balance, bool isWin, string currency)
        {
            Guid transactionId = Guid.NewGuid();

            var mockGameCalculator = new Mock<IGameCalculator>();

            mockGameCalculator
              .Setup((gameCalculator) => gameCalculator.CalculateRandomWin(betAmount))
              .Returns(_flipDataResponseFactory.Create(winAmount, isWin));

            var mockService = new Mock<ICasinoService>();

            mockService
                .Setup((service) => service.GetBalance(token, playerId, currency))
                .Returns(_getBalanceResponseFactory.Create(balance.ToString(), balance, currency, token, false, 0, "", null));

            mockService
                .Setup((service) => service.DoBet(betAmount, transactionId.ToString(), gameId, token, playerId, currency))
                .Returns(_doBetResponseFactory.Create(Guid.NewGuid().ToString(), balance - betAmount, currency, token, false, 0, "", null));

            mockService
                .Setup((service) => service.DoWin(winAmount, transactionId.ToString(), gameId, token, playerId, currency))
                .Returns(_doWinResponseFactory.Create(Guid.NewGuid().ToString(), balance - betAmount + winAmount, currency, token, false, 0, "", null));

            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup((unitOfWork) => unitOfWork.TransactionRepository.Add(new Transaction()));

            var processor = new Processor(mockUnitOfWork.Object, mockService.Object,
                SingletonProvider.ServiceProvider.GetService<IAuthProcessorResponseFactory>(),
                SingletonProvider.ServiceProvider.GetService<IErrorResponseFactory>(), new Mock<ILogger<Processor>>().Object,
                mockGameCalculator.Object, SingletonProvider.ServiceProvider.GetService<IFlipProcessorResponseFactory>());


            var processFlipResponse = processor.ProcessFlip(_flipProcessorFactory.Create(playerId, gameId, token, currency, transactionId, betAmount));

            Assert.That(processFlipResponse.ErrorResponse.ErrorCode == (int)ErrorCode.Success);
            Assert.That(processFlipResponse.ErrorResponse.Message == "Ok");
            Assert.That(processFlipResponse.Balance == balance - betAmount + winAmount);
            Assert.That(processFlipResponse.WinAmount == winAmount);
        }

        [TestCase(-1837, 388, "4357855ff4b58a4710fd050cdbd3e", 1, "USD")]
        [TestCase(-1837, 388, "4357855ff4b58a4710fd050cdbd3e", 1, "AMD")]
        [TestCase(-1837, 388, "4357855ff4b58a4710fd050cdbd3e", 1, "EUR")]
        public void Processor_Flip_Get_Balance_Has_Error(int playerId, int gameId, string token, decimal betAmount, string currency)
        {
            Guid transactionId = Guid.NewGuid();

            var mockService = new Mock<ICasinoService>();

            mockService
                .Setup((service) => service.GetBalance(token, playerId, currency))
                .Returns(_getBalanceResponseFactory.Create(null, null, null, null, true, 147, "", null));


            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup((unitOfWork) => unitOfWork.TransactionRepository.Add(new Transaction()));

            var processor = new Processor(mockUnitOfWork.Object, mockService.Object,
                SingletonProvider.ServiceProvider.GetService<IAuthProcessorResponseFactory>(),
                SingletonProvider.ServiceProvider.GetService<IErrorResponseFactory>(), new Mock<ILogger<Processor>>().Object,
                new Mock<IGameCalculator>().Object, SingletonProvider.ServiceProvider.GetService<IFlipProcessorResponseFactory>());


            var processFlipResponse = processor.ProcessFlip(_flipProcessorFactory.Create(playerId, gameId, token, currency, transactionId, betAmount));

            Assert.That(processFlipResponse.ErrorResponse.ErrorCode == (int)ErrorCode.GetBalance);
            Assert.That(processFlipResponse.ErrorResponse.Message == string.Format(Messages.Logger_Generic_Error, WebCallName.Flip));
            Assert.That(processFlipResponse.Balance == -1);
            Assert.That(processFlipResponse.WinAmount == -1);
        }

        [TestCase(-1837, 388, "4357855ff4b58a4710fd050cdbd3e", 1, 2, "USD")]
        [TestCase(-1837, 388, "4357855ff4b58a4710fd050cdbd3e", 2, 4, "AMD")]
        [TestCase(-1837, 388, "4357855ff4b58a4710fd050cdbd3e", 3, 6, "EUR")]
        public void Processor_Flip_DoBet_Has_Error(int playerId, int gameId, string token, decimal betAmount, decimal balance, string currency)
        {
            Guid transactionId = Guid.NewGuid();

            var mockService = new Mock<ICasinoService>();

            mockService
                 .Setup((service) => service.GetBalance(token, playerId, currency))
                 .Returns(_getBalanceResponseFactory.Create(balance.ToString(), balance, currency, token, false, 0, "", null));

            mockService
              .Setup((service) => service.DoBet(betAmount, transactionId.ToString(), gameId, token, playerId, currency))
              .Returns(_doBetResponseFactory.Create(null, null, null, null, true, 140, "", null));

            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup((unitOfWork) => unitOfWork.TransactionRepository.Add(new Transaction()));

            var processor = new Processor(mockUnitOfWork.Object, mockService.Object,
                SingletonProvider.ServiceProvider.GetService<IAuthProcessorResponseFactory>(),
                SingletonProvider.ServiceProvider.GetService<IErrorResponseFactory>(), new Mock<ILogger<Processor>>().Object,
                new Mock<IGameCalculator>().Object, SingletonProvider.ServiceProvider.GetService<IFlipProcessorResponseFactory>());


            var processFlipResponse = processor.ProcessFlip(_flipProcessorFactory.Create(playerId, gameId, token, currency, transactionId, betAmount));

            Assert.That(processFlipResponse.ErrorResponse.ErrorCode == (int)ErrorCode.DoBet);
            Assert.That(processFlipResponse.ErrorResponse.Message == string.Format(Messages.Logger_Generic_Error, WebCallName.Flip));
            Assert.That(processFlipResponse.Balance == -1);
            Assert.That(processFlipResponse.WinAmount == -1);
        }

        [TestCase(-1837, 388, "4357855ff4b58a4710fd050cdbd3e", 1, 2, 5000, "USD")]
        [TestCase(-1837, 388, "4357855ff4b58a4710fd050cdbd3e", 2, 4, 8000, "AMD")]
        [TestCase(-1837, 388, "4357855ff4b58a4710fd050cdbd3e", 3, 6, 1000, "USD")]
        [TestCase(-1837, 388, "4357855ff4b58a4710fd050cdbd3e", 1, 0, 5000, "AMD")]
        [TestCase(-1837, 388, "4357855ff4b58a4710fd050cdbd3e", 2, 0, 8000, "EUR")]
        [TestCase(-1837, 388, "4357855ff4b58a4710fd050cdbd3e", 3, 0, 1000, "USD")]
        public void Processor_Flip_DoWin_Has_Error_Rolleback_Success(int playerId, int gameId, string token, decimal betAmount, decimal winAmount, decimal balance,string currency)
        {
            Guid transactionId = Guid.NewGuid();

            var mockGameCalculator = new Mock<IGameCalculator>();

            mockGameCalculator
                .Setup((gameCalculator) => gameCalculator.CalculateRandomWin(betAmount))
                .Returns(_flipDataResponseFactory.Create(winAmount, true));

            var mockService = new Mock<ICasinoService>();

            mockService
                .Setup((service) => service.GetBalance(token, playerId, currency))
                .Returns(_getBalanceResponseFactory.Create(balance.ToString(), balance, currency, token, false, 0, "", null));

            mockService
                .Setup((service) => service.DoBet(betAmount, transactionId.ToString(), gameId, token, playerId, currency))
                .Returns(_doBetResponseFactory.Create(Guid.NewGuid().ToString(), balance - betAmount, currency, token, false, 0, "", null));

            mockService
                .Setup((service) => service.DoWin(winAmount, transactionId.ToString(), gameId, token, playerId, currency))
                .Returns(_doWinResponseFactory.Create(null, null, null, null, true, 120, "Can't dowin", null));
            mockService
                .Setup((service) => service.DoRolleback(transactionId.ToString(), gameId, token, playerId, currency))
                .Returns(_doRollebackResponseFactory.Create(Guid.NewGuid().ToString(), balance, currency, token, false, 0, "", null));

            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup((unitOfWork) => unitOfWork.TransactionRepository.Add(new Transaction()));

            var processor = new Processor(mockUnitOfWork.Object, mockService.Object,
                SingletonProvider.ServiceProvider.GetService<IAuthProcessorResponseFactory>(),
                SingletonProvider.ServiceProvider.GetService<IErrorResponseFactory>(), new Mock<ILogger<Processor>>().Object,
                mockGameCalculator.Object, SingletonProvider.ServiceProvider.GetService<IFlipProcessorResponseFactory>());



            var processFlipResponse = processor.ProcessFlip(_flipProcessorFactory.Create(playerId, gameId, token, currency, transactionId, betAmount));

            Assert.That(processFlipResponse.ErrorResponse.ErrorCode == (int)ErrorCode.DoWin);
            Assert.That(processFlipResponse.ErrorResponse.Message == string.Format(Messages.Logger_Generic_Error, WebCallName.Flip));
            Assert.That(processFlipResponse.Balance == -1);
            Assert.That(processFlipResponse.WinAmount == -1);
        }

        [TestCase(-1837, 388, "4357855ff4b58a4710fd050cdbd3e", 1, 2, 5000, true, "USD")]
        [TestCase(-1837, 388, "4357855ff4b58a4710fd050cdbd3e", 2, 4, 8000, true, "EUR")]
        [TestCase(-1837, 388, "4357855ff4b58a4710fd050cdbd3e", 3, 6, 1000, true, "EUR")]
        [TestCase(-1837, 388, "4357855ff4b58a4710fd050cdbd3e", 1, 0, 5000, true, "AMD")]
        [TestCase(-1837, 388, "4357855ff4b58a4710fd050cdbd3e", 2, 0, 8000, true, "AMD")]
        [TestCase(-1837, 388, "4357855ff4b58a4710fd050cdbd3e", 3, 0, 1000, true, "USD")]
        public void Processor_Flip_DoWin_Has_Error_Rolleback_Has_Error(int playerId, int gameId, string token, decimal betAmount, decimal winAmount, decimal balance, bool isWin, string currency)
        {
            Guid transactionId = Guid.NewGuid();

            var mockGameCalculator = new Mock<IGameCalculator>();

            mockGameCalculator
                .Setup((gameCalculator) => gameCalculator.CalculateRandomWin(betAmount))
                .Returns(_flipDataResponseFactory.Create(winAmount, isWin));

            var mockService = new Mock<ICasinoService>();

            mockService
                .Setup((service) => service.GetBalance(token, playerId, currency))
                .Returns(_getBalanceResponseFactory.Create(balance.ToString(), balance, currency, token, false, 0, "", null));

            mockService
                .Setup((service) => service.DoBet(betAmount, transactionId.ToString(), gameId, token, playerId, currency))
                .Returns(_doBetResponseFactory.Create(Guid.NewGuid().ToString(), balance - betAmount, currency, token, false, 0, "", null));

            mockService
                .Setup((service) => service.DoWin(winAmount, transactionId.ToString(), gameId, token, playerId, currency))
                .Returns(_doWinResponseFactory.Create(null, null, null, null, true, 120, "Can't dowin", null));
            mockService
                .Setup((service) => service.DoRolleback(transactionId.ToString(), gameId, token, playerId, currency))
                .Returns(_doRollebackResponseFactory.Create(null, null, null, null, true, 120, "Can't Rolleback", null));

            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup((unitOfWork) => unitOfWork.TransactionRepository.Add(new Transaction()));

            var processor = new Processor(mockUnitOfWork.Object, mockService.Object,
                SingletonProvider.ServiceProvider.GetService<IAuthProcessorResponseFactory>(),
                SingletonProvider.ServiceProvider.GetService<IErrorResponseFactory>(), new Mock<ILogger<Processor>>().Object,
                mockGameCalculator.Object, SingletonProvider.ServiceProvider.GetService<IFlipProcessorResponseFactory>());



            var processFlipResponse = processor.ProcessFlip(_flipProcessorFactory.Create(playerId, gameId, token, currency, transactionId, betAmount));

            Assert.That(processFlipResponse.ErrorResponse.ErrorCode == (int)ErrorCode.DoRolleback);
            Assert.That(processFlipResponse.ErrorResponse.Message == string.Format(Messages.Logger_Generic_Error, WebCallName.Flip));
            Assert.That(processFlipResponse.Balance == -1);
            Assert.That(processFlipResponse.WinAmount == -1);
        }

        [TestCase(-1837, 388, "4357855ff4b58a4710fd050cdbd3e", 1, 2, 5000, true, "USD")]
        [TestCase(-1837, 388, "4357855ff4b58a4710fd050cdbd3e", 2, 4, 8000, true, "AMD")]
        [TestCase(-1837, 388, "4357855ff4b58a4710fd050cdbd3e", 3, 6, 1000, true, "EUR")]
        [TestCase(-1837, 388, "4357855ff4b58a4710fd050cdbd3e", 1, 0, 5000, false, "USD")]
        [TestCase(-1837, 388, "4357855ff4b58a4710fd050cdbd3e", 2, 0, 8000, false, "EUR")]
        [TestCase(-1837, 388, "4357855ff4b58a4710fd050cdbd3e", 3, 0, 1000, false, "AMD")]
        public void Processor_Flip_Throws_Exception(int playerId, int gameId, string token, decimal betAmount, decimal winAmount, decimal balance, bool isWin, string currency)
        {
            Guid transactionId = Guid.NewGuid();

            var mockGameCalculator = new Mock<IGameCalculator>();

            mockGameCalculator
              .Setup((gameCalculator) => gameCalculator.CalculateRandomWin(betAmount))
              .Returns(_flipDataResponseFactory.Create(winAmount, isWin));

            var mockService = new Mock<ICasinoService>();

            mockService
                .Setup((service) => service.GetBalance(token, playerId, currency))
                .Returns(_getBalanceResponseFactory.Create(balance.ToString(), balance, currency, token, false, 0, "", null));

            mockService
                .Setup((service) => service.DoBet(betAmount, transactionId.ToString(), gameId, token, playerId, currency))
                .Returns(_doBetResponseFactory.Create(Guid.NewGuid().ToString(), balance - betAmount, currency, token, false, 0, "", null));

            mockService
                .Setup((service) => service.DoWin(winAmount, transactionId.ToString(), gameId, token, playerId, currency))
                .Returns(_doWinResponseFactory.Create(Guid.NewGuid().ToString(), balance - betAmount + winAmount, currency, token, false, 0, "", null));

            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup((unitOfWork) => unitOfWork.TransactionRepository.Add(new Transaction()));

            mockUnitOfWork
                .Setup((unitOfWork) => unitOfWork.Save())
                .Throws(new Exception("hello from ex"));

            var processor = new Processor(mockUnitOfWork.Object, mockService.Object,
                  SingletonProvider.ServiceProvider.GetService<IAuthProcessorResponseFactory>(),
                  SingletonProvider.ServiceProvider.GetService<IErrorResponseFactory>(), new Mock<ILogger<Processor>>().Object,
                  mockGameCalculator.Object, SingletonProvider.ServiceProvider.GetService<IFlipProcessorResponseFactory>());


            var processFlipResponse = processor.ProcessFlip(_flipProcessorFactory.Create(playerId, gameId, token, currency, transactionId, betAmount));

            Assert.That(processFlipResponse.ErrorResponse.ErrorCode == (int)ErrorCode.Generic);
            Assert.That(processFlipResponse.ErrorResponse.Message == string.Format(Messages.Logger_Generic_Error, WebCallName.Flip));
            Assert.That(processFlipResponse.Balance == -1);
            Assert.That(processFlipResponse.WinAmount == -1);
        }
    }
}
