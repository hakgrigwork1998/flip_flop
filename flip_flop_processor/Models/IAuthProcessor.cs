using System;
using System.Collections.Generic;
using System.Text;

namespace flip_flop_processor.Models
{
    public interface IAuthProcessor
    {
        int GameId { get;}
        string Token { get; }
        string Currency { get; }
    }
}
