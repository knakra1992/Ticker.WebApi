using Microsoft.AspNetCore.Mvc;
using SimulatorEngine.Business.Interfaces;
using SimulatorEngine.Business.Models;

namespace SimulatorEngine.Controllers
{
    [Route("api/tickers")]
    [ApiController]
    public class TickerController : ControllerBase
    {
        private readonly ITickerHelper _tickerHelper;

        public TickerController(ITickerHelper tickerHelper) =>
            _tickerHelper = tickerHelper;

        [HttpGet]
        public IActionResult GetTickers([FromQuery] string? symbol)
        {
            var tickers = _tickerHelper.GetTickers(symbol);

            return Ok(tickers);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewTicker(Ticker ticker)
        {
            var tickerId = await _tickerHelper.CreateNewTicker(ticker);

            return Ok(tickerId);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTicker(Ticker ticker)
        {
            await _tickerHelper.UpdateTicker(ticker);

            return NoContent();
        }
    }
}
