using CardDeckApi.Data;
using CardDeckApi.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace CardDeckApi
{
    public static class DbDataInitializer
    {
        public static void InitDbData(this WebApplication application)
        {
            using (var scope = application.Services.CreateScope())
            using (var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>())
            {
                var dataGenerator = scope.ServiceProvider.GetRequiredService<IDataGenerator>();
                if (dbContext.CardSuits.FirstOrDefault() == null)
                {
                    dbContext.CardSuits.AddRange(dataGenerator.CardSuits);
                }
                if (dbContext.CardStrengths.FirstOrDefault() == null)
                {
                    dbContext.CardStrengths.AddRange(dataGenerator.CardStrengths);
                }
                if (dbContext.Cards.FirstOrDefault() == null)
                {
                    dbContext.Cards.AddRange(dataGenerator.Cards);
                }

                dbContext.SaveChanges();
            }
        }
    }
}
