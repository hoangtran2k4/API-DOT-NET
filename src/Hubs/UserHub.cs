using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class UserHub : Hub
{
    public async Task SendUserActivity(string username, bool isActive)
    {
        // Broadcast trạng thái user này cho tất cả client khác
        await Clients.Others.SendAsync("ReceiveUserActivity", username, isActive);
    }
}
