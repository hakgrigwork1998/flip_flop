using System;
using System.Collections.Generic;
using System.Text;
using flip_flop_game_logic.Factories;
using flip_flop_game_logic.Models;
using Microsoft.Extensions.Configuration;

namespace flip_flop_game_logic.Logic
{
    public class GameCalculator : IGameCalculator
    {
        private readonly IFlipDataResponseFactory _flipDataResponseFactory;
        private readonly IConfiguration _configuration;

        public GameCalculator(IFlipDataResponseFactory flipDataResponseFactory, IConfiguration configuration)
        {
            _flipDataResponseFactory = flipDataResponseFactory;
            _configuration = configuration;
        }

        public IFlipDataResponse CalculateRandomWin(decimal betAmount)
        {
            decimal rate = decimal.Parse(_configuration.GetSection("FlipRate").Value);
            return _flipDataResponseFactory.Create(rate * betAmount, new Random().Next(0, 2) == 1);
        }
    }
}
