using SimulatorEngine.Business.Models;

namespace SimulatorEngine.Business.Interfaces
{
	public interface ISubscriptionHelper
	{
		Task UpdateTicker(Ticker data);

		Task TickerAdded(Ticker data);
	}
}

