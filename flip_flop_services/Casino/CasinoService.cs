using System.Threading.Tasks;
using flip_flop_services.Casino.Response;
using flip_flop_services.Casino.Request;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using flip_flop_services.Models.Casino.Response;
using flip_flop_services.Models.Casino.Request;
using System;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace flip_flop_services.Casino
{
    public class CasinoService : BaseService, ICasinoService
    {
        public CasinoService(IConfiguration configuration, HttpClient httpClient)
            : base(configuration, httpClient)
        {
            _httpClient.BaseAddress = new Uri(_configuration.GetSection("Casino_API:Base_Address").Value);
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public IAuthResponse Auth(int gameId, string token, string currency)
        {
            AuthRequest authRequest = new AuthRequest(gameId, token, currency);

            string json = JsonConvert.SerializeObject(authRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync(_configuration.GetSection("Casino_API:EndPoints:Authentication").Value, content).Result;

            var responseString = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<AuthResponse>(responseString);
        }

        public async Task<IAuthResponse> AuthAsync(int gameId, string token, string currency)
        {
            AuthRequest authRequest = new AuthRequest(gameId, token, currency);

            string json = JsonConvert.SerializeObject(authRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_configuration.GetSection("Casino_API:EndPoints:Authentication").Value, content);

            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<AuthResponse>(responseString);
        }

        public IDoBetResponse DoBet(decimal betAmount, string transactionId, int gameId, string token, int playerId, string currencyId)
        {
            DoBetRequest doBetRequest = new DoBetRequest(betAmount, transactionId, gameId, token, playerId, currencyId);

            string json = JsonConvert.SerializeObject(doBetRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync(_configuration.GetSection("Casino_API:EndPoints:DoBet").Value, content).Result;

            var responseString = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<DoBetResponse>(responseString);
        }

        public async Task<IDoBetResponse> DoBetAsync(decimal betAmount, string transactionId, int gameId, string token, int playerId, string currencyId)
        {
            DoBetRequest doBetRequest = new DoBetRequest(betAmount, transactionId, gameId, token, playerId, currencyId);

            string json = JsonConvert.SerializeObject(doBetRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_configuration.GetSection("Casino_API:EndPoints:DoBet").Value, content);

            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<DoBetResponse>(responseString);
        }

        public IDoRollebackResponse DoRolleback(string transactionId, int gameId, string token, int playerId, string currencyId)
        {
            DoRollebackRequest doRollebackRequest = new DoRollebackRequest(transactionId, gameId, token, playerId, currencyId);

            string json = JsonConvert.SerializeObject(doRollebackRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync(_configuration.GetSection("Casino_API:EndPoints:DoRolleback").Value, content).Result;

            var responseString = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<DoRollebackResponse>(responseString);
        }

        public async Task<IDoRollebackResponse> DoRollebackAsync(string transactionId, int gameId, string token, int playerId, string currencyId)
        {
            DoRollebackRequest doRollebackRequest = new DoRollebackRequest(transactionId, gameId, token, playerId, currencyId);

            string json = JsonConvert.SerializeObject(doRollebackRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_configuration.GetSection("Casino_API:EndPoints:DoRolleback").Value, content);

            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<DoRollebackResponse>(responseString);
        }

        public IDoWinResponse DoWin(decimal winAmount, string transactionId, int gameId, string token, int playerId, string currencyId)
        {
            DoWinRequest doWinRequest = new DoWinRequest(winAmount, transactionId, gameId, token, playerId, currencyId);

            string json = JsonConvert.SerializeObject(doWinRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync(_configuration.GetSection("Casino_API:EndPoints:DoWin").Value, content).Result;

            var responseString = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<DoWinResponse>(responseString);
        }

        public async Task<IDoWinResponse> DoWinAsync(decimal winAmount, string transactionId, int gameId, string token, int playerId, string currencyId)
        {
            DoWinRequest doWinRequest = new DoWinRequest(winAmount, transactionId, gameId, token, playerId, currencyId);

            string json = JsonConvert.SerializeObject(doWinRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_configuration.GetSection("Casino_API:EndPoints:DoWin").Value, content);

            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<DoWinResponse>(responseString);
        }

        public IGetBalanceResponse GetBalance(string token, int playerId, string currency)
        {
            GetBalanceRequest getBalanceRequest = new GetBalanceRequest(token, playerId, currency);

            string json = JsonConvert.SerializeObject(getBalanceRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync(_configuration.GetSection("Casino_API:EndPoints:GetBalance").Value, content).Result;

            var responseString = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<GetBalanceResponse>(responseString);
        }

        public async Task<IGetBalanceResponse> GetBalanceAsync(string token, int playerId, string currency)
        {
            GetBalanceRequest getBalanceRequest = new GetBalanceRequest(token, playerId, currency);

            string json = JsonConvert.SerializeObject(getBalanceRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_configuration.GetSection("Casino_API:EndPoints:GetBalance").Value, content);

            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<GetBalanceResponse>(responseString);
        }
    }
}
