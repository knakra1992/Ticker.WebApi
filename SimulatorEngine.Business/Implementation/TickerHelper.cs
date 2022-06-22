using Microsoft.Extensions.Caching.Memory;
using SimulatorEngine.Business.Interfaces;
using SimulatorEngine.Business.Models;

namespace SimulatorEngine.Business.Implementation
{
	public class TickerHelper : ITickerHelper
	{
        private readonly ISubscriptionHelper _subscriptionHelper;

        private readonly IMemoryCache _memoryCache;

        public TickerHelper(ISubscriptionHelper subscriptionHelper, IMemoryCache memoryCache) =>
            (_subscriptionHelper, _memoryCache) = (subscriptionHelper, memoryCache);

        public async Task<string> CreateNewTicker(Ticker ticker)
        {
            var tickerId = createOrUpdate(ticker);
            await _subscriptionHelper.TickerAdded(ticker);

            return tickerId;
        }

        public IEnumerable<Ticker> GetTickers(string? symbol = null)
        {
            var tickers = _memoryCache.Get<Dictionary<string, Ticker>>("tickers") ?? new Dictionary<string, Ticker>();

            var tickerResults = new List<Ticker>();

            if (string.IsNullOrEmpty(symbol))
            {
                foreach (var ticker in tickers)
                {
                    tickerResults.Add(ticker.Value);
                }
            }
            else
            {
                tickerResults.Add(tickers[symbol]);
            }

            return tickerResults;
        }

        public async Task UpdateTicker(Ticker ticker)
        {
            createOrUpdate(ticker);

            await _subscriptionHelper.UpdateTicker(ticker);
        }

        private string createOrUpdate(Ticker ticker)
        {
            var exists = _memoryCache.TryGetValue("tickers", out Dictionary<string, Ticker> existingTickers);

            if (!exists)
            {
                ticker.TickerId = Guid.NewGuid().ToString();

                _memoryCache.Set<Dictionary<string, Ticker>>("tickers", new Dictionary<string, Ticker>
                {
                    { ticker.Symbol, ticker }
                });

                return ticker.TickerId;
            }
            else
            {
                if (existingTickers.TryGetValue(ticker.Symbol, out Ticker? existingTicker))
                {
                    existingTickers[ticker.Symbol] = ticker;
                }
                else
                {
                    existingTickers.Add(ticker.Symbol, ticker);
                }

                return existingTickers[ticker.Symbol].TickerId;
            }
        }
    }
}

