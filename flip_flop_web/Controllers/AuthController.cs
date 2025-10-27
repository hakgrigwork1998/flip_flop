using flip_flop_core.Enums;
using flip_flop_core.Messages;
using flip_flop_processor.Factories;
using flip_flop_processor.Processors;
using flip_flop_web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace flip_flop_web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthProcessorFactory _authProcessorFactory;
        private readonly IProcessor _processor;
        private readonly ILogger _logger;

        public AuthController(IAuthProcessorFactory authProcessorFactory, IProcessor processor, ILogger<AuthController> logger)
        {
            _authProcessorFactory = authProcessorFactory;
            _processor = processor;
            _logger = logger;
        }

        [HttpPost]
        public JsonResult Auth([FromBody]AuthRequest authRequest)
        {
            _logger.LogInformation(Messages.Logger_Started_Request, WebCallName.Authentication, JsonConvert.SerializeObject(authRequest));

            var authProcessorModel = _authProcessorFactory.Create(authRequest.GameId, authRequest.Token,authRequest.Currency);
            var processAuthResponse = _processor.ProcessAuth(authProcessorModel);

            _logger.LogInformation(Messages.Logger_Ended_Request, WebCallName.Authentication, JsonConvert.SerializeObject(processAuthResponse));

            return Json(processAuthResponse);
        }
    }
}