using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class UserHub : Hub
{
    public async Task SendUserActivity(string username, bool isActive)
    {
        await Clients.Others.SendAsync("ReceiveUserActivity", username, isActive);
    }
}
