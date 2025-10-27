using System;
using System.Collections.Generic;
using System.Text;

namespace flip_flop_core.Enums
{
    public enum ErrorCode
    {
        Success=0,
        Generic=1,
        Authentication = 2,
        GetBalance =3,
        DoBet=4,
        DoWin=5,
        DoRolleback=6
    }
}
