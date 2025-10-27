using System;
using System.Collections.Generic;
using System.Text;

namespace flip_flop_game_logic.Models
{
    public interface IFlipDataResponse
    {
        decimal WinAmount { get; }
        bool IsWin { get; }
    }
}
