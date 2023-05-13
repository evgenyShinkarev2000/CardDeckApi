using System.Collections;
using System.Collections.Generic;

namespace CardDeckApi.Data
{
    public class CardDeck : AppDbEnity
    {
        public string Name { get; set; } = default!;
        public ICollection<Card> Cards { get; set; } = new HashSet<Card>();
    }
}
