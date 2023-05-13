using CardDeckApi.Data;
using CardDeckApi.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace CardDeckApi.Controllers
{
    [Route("api/CardDeck")]
    public class CardDeckController : Controller
    {
        private readonly AppDbContext appDbContext;
        private readonly IShuffler shuffler;
        private readonly string nameRequiredMessage = "Query parameter name required";

        public CardDeckController(AppDbContext appDbContext, IShuffler shuffler)
        {
            this.appDbContext = appDbContext;
            this.shuffler = shuffler;
        }

        [HttpGet]
        public IActionResult GetAllDecks()
        {
            return Ok(IncludeAll(appDbContext.CardDecks));
        }

        [HttpGet("Names")]
        public IActionResult GetDesckNames()
        {
            return Ok(appDbContext.CardDecks);
        }

        [HttpPost("Create")]
        public IActionResult Create([FromQuery] string name)
        {
            if (name == null)
            {
                return BadRequest(nameRequiredMessage);
            }
            if (appDbContext.CardDecks.FirstOrDefault(cd => cd.Name == name) != null)
            {
                return BadRequest("name must be unique");
            }

            var cards = appDbContext.Cards;
            var cardDeck = new CardDeck() { Name = name, Cards = cards.ToArray() };
            appDbContext.CardDecks.Add(cardDeck);

            appDbContext.SaveChanges();

            var cardDeckNav = IncludeAll(appDbContext.CardDecks).First(cd => cd.Id == cardDeck.Id);

            return Ok(cardDeckNav);
        }

        [HttpDelete("Search")]
        public IActionResult RemoveByName([FromQuery] string name)
        {
            if (name == null)
            {
                return BadRequest(nameRequiredMessage);
            }
            var cardDeck = IncludeAll(appDbContext.CardDecks).FirstOrDefault(cd => cd.Name == name);
            if (cardDeck == null)
            {
                return NotFound();
            }

            appDbContext.CardDecks.Remove(cardDeck);
            appDbContext.SaveChanges();

            return Ok(cardDeck);
        }

        [HttpGet("Search")]
        public IActionResult GetByName([FromQuery] string name)
        {
            if (name == null)
            {
                return BadRequest(nameRequiredMessage);
            }
            var cardDeck = IncludeAll(appDbContext.CardDecks).FirstOrDefault(cd => cd.Name == name);
            if (cardDeck == null)
            {
                return NotFound();
            }

            return Ok(cardDeck);
        }

        [HttpPost("Shuffle/RandomSort")]
        public IActionResult RandomSortShuffle([FromQuery] string name)
        {
            if (name == null)
            {
                return BadRequest(nameRequiredMessage);
            }
            var cardDeck = IncludeAll(appDbContext.CardDecks).FirstOrDefault(cd => cd.Name == name);
            if (cardDeck == null)
            {
                return NotFound();
            }

            cardDeck.Cards = shuffler.OrderByRandom(cardDeck.Cards).ToArray();

            appDbContext.SaveChanges();

            return Ok(cardDeck);
        }

        [HttpPost("Shuffle/Fan")]
        public IActionResult BeginnerShuffle([FromQuery] string name, [FromQuery] int repeatCount = 1)
        {
            if (name == null)
            {
                return BadRequest(nameRequiredMessage);
            }
            if (repeatCount < 0)
            {
                return BadRequest($"{nameof(repeatCount)} must be > 0");
            }
            var cardDeck = IncludeAll(appDbContext.CardDecks).FirstOrDefault(cd => cd.Name == name);
            if (cardDeck == null)
            {
                return NotFound();
            }

            var shuffled = cardDeck.Cards.ToArray();
            foreach (var i in Enumerable.Range(0, repeatCount))
            {
                shuffled = shuffler.FanShuffle(shuffled).ToArray();
            }

            cardDeck.Cards = shuffled;

            appDbContext.SaveChanges();

            return Ok(cardDeck);
        }

        [HttpPost("OrderById")]
        public IActionResult OrderCardDeckById([FromQuery] string name)
        {
            if (name == null)
            {
                return BadRequest(nameRequiredMessage);
            }
            var cardDeck = IncludeAll(appDbContext.CardDecks).FirstOrDefault(cd => cd.Name == name);
            if (cardDeck == null)
            {
                return NotFound();
            }


            cardDeck.Cards = cardDeck.Cards.OrderBy(c => c.Id).ToArray();
            appDbContext.SaveChanges();

            return Ok(cardDeck);
        }

        private IQueryable<CardDeck> IncludeAll(IQueryable<CardDeck> cardDecks)
        {
            return cardDecks
                .Include(cd => cd.Cards).ThenInclude(c => c.Suit)
                .Include(cd => cd.Cards).ThenInclude(c => c.CardStrength);
        }
    }
}
