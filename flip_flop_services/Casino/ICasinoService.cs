using flip_flop_services.Casino.Request;
using flip_flop_services.Casino.Response;
using flip_flop_services.Models.Casino.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace flip_flop_services.Casino
{
    public interface ICasinoService
    {
        IAuthResponse Auth(int gameId, string token,string currency);
        Task<IAuthResponse> AuthAsync(int gameId, string token,string currency);
        IGetBalanceResponse GetBalance(string token, int playerId, string currency);
        Task<IGetBalanceResponse> GetBalanceAsync(string token, int playerId, string currency);
        IDoBetResponse DoBet(decimal betAmount, string transactionId, int gameId, string token, int playerId, string currencyId);
        Task<IDoBetResponse> DoBetAsync(decimal betAmount, string transactionId, int gameId, string token, int playerId, string currencyId);
        IDoWinResponse DoWin(decimal winAmount, string transactionId, int gameId, string token, int playerId, string currencyId);
        Task<IDoWinResponse> DoWinAsync(decimal winAmount, string transactionId, int gameId, string token, int playerId, string currencyId);
        IDoRollebackResponse DoRolleback(string transactionId, int gameId, string token, int playerId, string currencyId);
        Task<IDoRollebackResponse> DoRollebackAsync(string transactionId, int gameId, string token, int playerId, string currencyId);
    }
}
