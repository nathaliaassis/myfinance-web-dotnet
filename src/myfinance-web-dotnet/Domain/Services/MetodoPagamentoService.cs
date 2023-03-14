using Microsoft.EntityFrameworkCore;
using myfinance_web_dotnet.Domain.Entities;
using myfinance_web_dotnet.Domain.Services.Interfaces;
using myfinance_web_dotnet.Models;

namespace myfinance_web_dotnet.Domain.Services
{
  public class MetodoPagamentoService : IMetodoPagamentoService
  {
    private readonly MyFinanceDbContext _dbContext;

    public MetodoPagamentoService(MyFinanceDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    List<MetodoPagamentoModel> IMetodoPagamentoService.ListarMetodos()
    {
      var result = new List<MetodoPagamentoModel>();
      var dbSet = _dbContext.MetodoPagamento;

      foreach (var item in dbSet)
      {
        var itemMetodoPagamento = new MetodoPagamentoModel()
        {
          Id = item.Id,
          Tipo = item.Tipo
        };

        result.Add(itemMetodoPagamento);
      }

      return result;
    }
  }
}