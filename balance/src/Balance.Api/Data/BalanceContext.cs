using Microsoft.EntityFrameworkCore;
using BalanceApi.Models;

namespace BalanceApi.Data;

public class BalanceContext : DbContext
{
    public BalanceContext(DbContextOptions<BalanceContext> options) : base(options) { }

    public DbSet<Account> Accounts { get; set; }
}
