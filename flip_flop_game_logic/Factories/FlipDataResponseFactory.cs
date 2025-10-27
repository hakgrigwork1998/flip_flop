using System;
using System.Collections.Generic;
using System.Text;
using flip_flop_game_logic.Models;

namespace flip_flop_game_logic.Factories
{
    public class FlipDataResponseFactory : IFlipDataResponseFactory
    {
        public IFlipDataResponse Create(decimal winAmount, bool isWin)
        {
            return new FlipDataResponse
            {
                WinAmount = winAmount,
                IsWin = isWin
            };
        }
    }
}
