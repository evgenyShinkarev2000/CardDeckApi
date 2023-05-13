using Microsoft.AspNetCore.SignalR;

namespace CardDeckApi.Data
{
    public class CardStrength : AppDbEnity
    {
        public string Name { get; set; } = default!;
    }
}
