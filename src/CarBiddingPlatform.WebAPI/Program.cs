using CarBiddingPlatform.Application.Commands.PlaceBid;
using CarBiddingPlatform.Application.Interfaces;
using CarBiddingPlatform.Infrastructure.Data;
using CarBiddingPlatform.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CarBiddingPlatform.WebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<BiddingDbContext>(options => 
            options.UseSqlServer(connectionString));
        // Add services to the container.
        
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<BiddingDbContext>();
        builder.Services.AddScoped<IAuctionRepository, AuctionRepository>();
        builder.Services.AddScoped<PlaceBidCommandHandler>();
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

        app.Run();
    }
}