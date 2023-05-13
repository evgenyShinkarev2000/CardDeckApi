using CardDeckApi.Data;
using System.Collections.Generic;
using System.Linq;

namespace CardDeckApi.Domain
{
    public class DataGenerator : IDataGenerator
    {
        private static readonly string[] cardSuitNames = new string[]
        {
            "hearts",
            "diamonds",
            "spades",
            "club",
        };
        private static readonly string[] cardStrengthNames = new string[]
        {
            "2", "3", "4", "5", "6", "7", "8", "9", "10", "jack", "queen", "king", "ace",
        };

        private static readonly CardSuit[] cardSuits;
        private static readonly CardStrength[] cardStrengths;
        private static readonly Card[] cards;

        static DataGenerator()
        {
            cardSuits = cardSuitNames
                .Select((csn, i) => new CardSuit() { Id = i + 1, Suit = csn })
                .ToArray();
            cardStrengths = cardStrengthNames
                .Select((csn, i) => new CardStrength() { Id = i + 1, Name = csn })
                .ToArray();

            var i = 1;
            var cardsList = new List<Card>(cardSuits.Length * cardStrengths.Length);

            foreach (var cardStrength in cardStrengths)
            {
                foreach (var cardSuit in cardSuits)
                {
                    cardsList.Add(new Card()
                    {
                        Id = i,
                        Suit = cardSuit,
                        CardStrength = cardStrength,
                    });
                    ++i;
                }
            }

            cards = cardsList.ToArray();
        }

        public IEnumerable<CardStrength> CardStrengths => cardStrengths;

        public IEnumerable<CardSuit> CardSuits => cardSuits;

        public IEnumerable<Card> Cards => cards;
    }
}
