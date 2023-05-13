using CardDeckApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace CardDeckApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<CardSuit> CardSuits { get; set; }
        public DbSet<CardStrength> CardStrengths { get; set; }
        public DbSet<CardDeck> CardDecks { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
