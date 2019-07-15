using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Cell.Core.SignalR
{
    public abstract class MainHub : Hub
    {
        private readonly ILogger<MainHub> _logger;

        protected MainHub(ILogger<MainHub> logger)
        {
            _logger = logger;
        }

        public override Task OnConnectedAsync()
        {
            var currentUserId = Context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            _logger.LogInformation(string.Join(",", Context.User?.Claims));
            _logger.LogInformation("Connection {0} connected and added to group of user {1}", Context.ConnectionId, currentUserId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            if (exception != null)
            {
                _logger.LogError(exception, "{0} {1}", exception.Message, exception.StackTrace);
            }
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SubscribeSession(Guid registrationId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, registrationId.ToString());
        }

        public async Task UnsubscribeSession(Guid registrationId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, registrationId.ToString());
        }
    }
}