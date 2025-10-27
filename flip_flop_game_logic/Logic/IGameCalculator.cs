using flip_flop_game_logic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace flip_flop_game_logic.Logic
{
    public interface IGameCalculator
    {
        IFlipDataResponse CalculateRandomWin(decimal betAmount);
    }
}
