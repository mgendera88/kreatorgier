using Microsoft.AspNetCore.SignalR;

namespace familiada.Hubs
{
    public class WynikiHub : Hub
    {
        public async Task OdswiezWyniki()
        {
            await Clients.All.SendAsync("OdswiezWyniki");
        }
        public async Task OdejmijSzanse()
        {
            await Clients.All.SendAsync("OdswiezWyniki");
        }
        public async Task przyciskrunda()
        {
            await Clients.All.SendAsync("OdswiezWyniki");
        }
    }
}