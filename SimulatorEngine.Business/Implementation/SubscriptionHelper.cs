using System;
using Microsoft.AspNetCore.SignalR;
using SimulatorEngine.Business.Hubs;
using SimulatorEngine.Business.Interfaces;
using SimulatorEngine.Business.Models;

namespace SimulatorEngine.Business.Implementation
{
	public class SubscriptionHelper : ISubscriptionHelper
	{
        private readonly IHubContext<SourceHub> _hubContext;

		public SubscriptionHelper(IHubContext<SourceHub> hubContext)
		{
            _hubContext = hubContext;
		}

        public async Task TickerAdded(Ticker data)
        {
            await _hubContext.Clients.Group("TickerAdded").SendAsync("TickerAdded", data);
        }

        public async Task UpdateTicker(Ticker data)
        {
            await _hubContext.Clients.Group(data.Symbol).SendAsync("RefreshTicker", data);
        }
    }
}

