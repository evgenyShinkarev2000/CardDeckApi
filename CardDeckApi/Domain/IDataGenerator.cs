using CardDeckApi.Data;
using System.Collections.Generic;

namespace CardDeckApi.Domain
{
    public interface IDataGenerator
    {
        public IEnumerable<CardStrength> CardStrengths { get; }
        public IEnumerable<CardSuit> CardSuits { get; }
        public IEnumerable<Card> Cards { get; }
    }
}
