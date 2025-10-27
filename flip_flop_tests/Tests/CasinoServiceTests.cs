using flip_flop_services.Casino;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
using flip_flop_core.Provider;
using System.Net.Http;
using System;
using System.Threading.Tasks;

namespace flip_flop_tests.Tests
{
    [TestFixture(Category = "Casino Service")]
    public class CasinoServiceTests
    {
        private ICasinoService _casinoService;

        [SetUp]
        public void SetUp()
        {
            _casinoService = new CasinoService(SingletonConfiguration.Configuration, new HttpClient());
        }

        [TestCase(388, "4357855ff4b58a4710fd050cdbd3e", "USD")]
        [TestCase(388, "4357855ff4b58a4710fd050cdbd3e", "AMD")]
        public void Casino_Service_Auth_Success(int gameId, string token, string currency)
        {
            var authResponse = _casinoService.Auth(gameId, token, currency);

            Assert.That(!authResponse.HasError);
        }

        [TestCase(388, "4357855ff4b58a4710fd050cdbd3e", "USD")]
        [TestCase(388, "4357855ff4b58a4710fd050cdbd3e", "AMD")]
        public async Task Casino_Service_Auth_Success_Async(int gameId, string token, string currency)
        {
            var authResponse = await _casinoService.AuthAsync(gameId, token, currency);

            Assert.That(!authResponse.HasError);
        }

        [TestCase(388, "4357855ff4b58sadsasdaa4710fd050cdbd3e", "USD")]
        [TestCase(388, "4357855ff4b58sadsasdaa4710fd050cdbd3e", "EUR")]
        public void Casino_Service_Auth_Wrong_Credential(int gameId, string token, string currency)
        {
            var authResponse = _casinoService.Auth(gameId, token, currency);

            Assert.That(authResponse.HasError);
        }

        [TestCase(388, "4357855ff4b58sadsasdaa4710fd050cdbd3e", "USD")]
        [TestCase(388, "4357855ff4b58sadsasdaa4710fd050cdbd3e", "EUR")]
        public async Task Casino_Service_Auth_Wrong_Credential_Async(int gameId, string token, string currency)
        {
            var authResponse = await _casinoService.AuthAsync(gameId, token, currency);

            Assert.That(authResponse.HasError);
        }


        [TestCase(1, 388, "4357855ff4b58a4710fd050cdbd3e", -1837, "USD")]
        public void Casino_Service_DoBet_Success(decimal betAmount, int gameId, string token, int playerId, string currencyId)
        {
            var doBetResponse = _casinoService.DoBet(betAmount, Guid.NewGuid().ToString(), gameId, token, playerId, currencyId);

            Assert.That(!doBetResponse.HasError);
        }

        [TestCase(1, 388, "4357855ff4b58a4710fd050cdbd3e", -1837, "USD")]
        public async Task Casino_Service_DoBet_Success_Async(decimal betAmount, int gameId, string token, int playerId, string currencyId)
        {
            var doBetResponse = await _casinoService.DoBetAsync(betAmount, Guid.NewGuid().ToString(), gameId, token, playerId, currencyId);

            Assert.That(!doBetResponse.HasError);
        }

        [TestCase(1, 388, "4357855ff4b58a4710fd050cdbd3e", -1837, "USD")]
        public void Casino_Service_DoWin_Success(decimal winAmount, int gameId, string token, int playerId, string currencyId)
        {
            var doWinResponse = _casinoService.DoWin(winAmount, Guid.NewGuid().ToString(), gameId, token, playerId, currencyId);

            Assert.That(!doWinResponse.HasError);
        }

        [TestCase(1, 388, "4357855ff4b58a4710fd050cdbd3e", -1837, "USD")]
        public async Task Casino_Service_DoWin_Success_Async(decimal winAmount, int gameId, string token, int playerId, string currencyId)
        {
            var doWinResponse = await _casinoService.DoWinAsync(winAmount, Guid.NewGuid().ToString(), gameId, token, playerId, currencyId);

            Assert.That(!doWinResponse.HasError);
        }

        [TestCase("4357855ff4b58a4710fd050cdbd3e", -1837, "USD")]
        public void Casino_Service_GetBalance_Success(string token, int playerId, string currency)
        {
            var getBalanceResponse = _casinoService.GetBalance(token, playerId, currency);

            Assert.That(!getBalanceResponse.HasError);
        }

        [TestCase("4357855ff4b58a4710fd050cdbd3e", -1837, "USD")]
        public async Task Casino_Service_GetBalance_Success_Async(string token, int playerId, string currency)
        {
            var getBalanceResponse = await _casinoService.GetBalanceAsync(token, playerId, currency);

            Assert.That(!getBalanceResponse.HasError);
        }


        [TestCase(1, 388, "4357855ff4b58a4710fd050cdbd3e", -1837, "USD")]
        public void Casino_Service_DoRolleback_Success(decimal betAmount, int gameId, string token, int playerId, string currencyId)
        {
            string transactionId = Guid.NewGuid().ToString();

            var doBetResponse = _casinoService.DoBet(betAmount, transactionId, gameId, token, playerId, currencyId);

            Assert.That(!doBetResponse.HasError);

            var doRolleback = _casinoService.DoRolleback(transactionId, gameId, token, playerId, currencyId);

            Assert.That(!doRolleback.HasError);
        }

        [TestCase(1, 388, "4357855ff4b58a4710fd050cdbd3e", -1837, "USD")]
        public async Task Casino_Service_DoRolleback_Success_Async(decimal betAmount, int gameId, string token, int playerId, string currencyId)
        {
            string transactionId = Guid.NewGuid().ToString();

            var doBetResponse = await _casinoService.DoBetAsync(betAmount, transactionId, gameId, token, playerId, currencyId);

            Assert.That(!doBetResponse.HasError);

            var doRolleback = await _casinoService.DoRollebackAsync(transactionId, gameId, token, playerId, currencyId);

            Assert.That(!doRolleback.HasError);
        }
    }
}
