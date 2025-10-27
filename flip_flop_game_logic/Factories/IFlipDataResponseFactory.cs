using flip_flop_game_logic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace flip_flop_game_logic.Factories
{
    public interface IFlipDataResponseFactory
    {
        IFlipDataResponse Create(decimal winAmount, bool isWin);
    }
}
