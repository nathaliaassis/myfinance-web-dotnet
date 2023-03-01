using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using myfinance_web_dotnet.Domain.Entities;

namespace myfinance_web_dotnet
{
  public class MyFinanceDbContext : DbContext
  {
    public DbSet<PlanoConta> PlanoConta { get; set; }
    public DbSet<Transacao> Transacao { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      var connectionsString = "Server=0.0.0.0,1433;Database=master;Trusted_Connection=True;TrustServerCertificate=True";
      optionsBuilder.UseSqlServer(connectionsString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }

  }
}