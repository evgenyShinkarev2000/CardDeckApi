using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CardDeckApi.Data
{
    public class Card : AppDbEnity
    {
        public CardSuit Suit { get; set; } = default!;
        public CardStrength CardStrength { get; set; } = default!;
        [JsonIgnore]
        public ICollection<CardDeck> UsedInDecks { get; set; } = new HashSet<CardDeck>();

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
