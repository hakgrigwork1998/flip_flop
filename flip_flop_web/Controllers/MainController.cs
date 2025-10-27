using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class MainController : Controller
    {
        private readonly IProcessor _processor;
        private readonly IFlipProcessorFactory _flipProcessorFactory;
        private readonly ILogger _logger;

        public MainController(IFlipProcessorFactory flipProcessorFactory, IProcessor processor, ILogger<MainController> logger)
        {
            _flipProcessorFactory = flipProcessorFactory;
            _processor = processor;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult FlipFlop()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Flip([FromBody]FlipFlopRequest flipFlopRequest)
        {
            _logger.LogInformation(Messages.Logger_Started_Request, WebCallName.Flip, JsonConvert.SerializeObject(flipFlopRequest));

            var flipProcessorModel = _flipProcessorFactory.Create(flipFlopRequest.PlayerId, flipFlopRequest.GameId, flipFlopRequest.Token, flipFlopRequest.Currency, Guid.NewGuid(), flipFlopRequest.BetAmount);
            var processFlipResponse = _processor.ProcessFlip(flipProcessorModel);

            _logger.LogInformation(Messages.Logger_Ended_Request, WebCallName.Flip, JsonConvert.SerializeObject(processFlipResponse));

            return Json(processFlipResponse);
        }
    }
}