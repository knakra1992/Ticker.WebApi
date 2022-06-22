using System;
using SimulatorEngine.Business.Models;

namespace SimulatorEngine.Business.Interfaces
{
	public interface ITickerHelper
	{
		IEnumerable<Ticker> GetTickers(string symbol);

		Task<string> CreateNewTicker(Ticker ticker);

		Task UpdateTicker(Ticker ticker);
	}
}
