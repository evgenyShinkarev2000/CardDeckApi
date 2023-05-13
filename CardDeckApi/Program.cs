using CardDeckApi.Data;
using CardDeckApi.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace CardDeckApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddTransient<IDataGenerator, DataGenerator>();
            builder.Services.AddTransient<IShuffler, Shuffler>();
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                var sqlConntectionString = builder.Configuration.GetConnectionString("SqlLite");
                if (sqlConntectionString == null)
                {
                    throw new Exception("Cant get SqlLite connection string");
                }
                options.UseSqlite(sqlConntectionString);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.InitDbData();

            app.Run();
        }
    }
}