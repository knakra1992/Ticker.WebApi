using System;
using Microsoft.AspNetCore.SignalR;

namespace SimulatorEngine.Business.Hubs
{
	public class SourceHub : Hub
	{
        public Task JoinGroup(string groupId)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, groupId);
        }

        public Task LeaveGroup(string groupId)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, groupId);
        }
    }
}

