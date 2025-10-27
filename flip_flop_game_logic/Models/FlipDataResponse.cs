using System;
using System.Collections.Generic;
using System.Text;

namespace flip_flop_game_logic.Models
{
    public class FlipDataResponse : IFlipDataResponse
    {
        public decimal WinAmount { get; set; }

        public bool IsWin { get; set; }
    }
}
