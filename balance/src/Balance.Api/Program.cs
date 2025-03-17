using BalanceApi.Data;
using BalanceApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<BalanceContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection")
    , new MySqlServerVersion(new Version(8, 0, 41))));

builder.Services.AddHostedService<KafkaConsumerService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/balances/{account_id}", async (string account_id, BalanceContext context) =>
{
    var account = await context.Accounts.FindAsync(account_id);
    return account != null ? Results.Ok(account) : Results.NotFound();
});

app.Run();
