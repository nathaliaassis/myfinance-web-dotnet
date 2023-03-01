using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myfinance_web_dotnet.Domain.Services.Interfaces;
using myfinance_web_dotnet.Models;

namespace myfinance_web_dotnet.Domain.Services
{
  public class PlanoContaService : IPlanoContaService
  {
    private readonly MyFinanceDbContext _dbContext;

    public PlanoContaService(MyFinanceDbContext dbContext)
    {
      _dbContext = dbContext;
    }
    public List<PlanoContaModel> ListarRegistros()
    {
      var result = new List<PlanoContaModel>();
      var dbSet = _dbContext.PlanoConta;

      foreach (var item in dbSet)
      {
        var itemPlanoConta = new PlanoContaModel()
        {
          Id = item.Id,
          Descricao = item.Descricao,
          Tipo = item.Tipo,
        };

        result.Add(itemPlanoConta);
      }

      return result;
    }
  }
}