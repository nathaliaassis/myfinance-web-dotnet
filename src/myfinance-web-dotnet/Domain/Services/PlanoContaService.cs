using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using myfinance_web_dotnet.Domain.Entities;
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
    public void Excluir(int id)
    {

      var dbSet = _dbContext.PlanoConta;
      var item = dbSet.Where(x => x.Id == id).First();

      _dbContext.Attach(item);
      _dbContext.Remove(item);
      _dbContext.SaveChanges();
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

    public PlanoContaModel RetornarRegistro(int id)
    {
      var item = _dbContext.PlanoConta.Where(x => x.Id == id).First();

      var itemPlanoConta = new PlanoContaModel()
      {
        Id = item.Id,
        Descricao = item.Descricao,
        Tipo = item.Tipo,
      };

      return itemPlanoConta;
    }

    public void Salvar(PlanoContaModel model)
    {
      var dbSet = _dbContext.PlanoConta;

      var entidade = new PlanoConta()
      {
        Id = model.Id,
        Descricao = model.Descricao,
        Tipo = model.Tipo,
      };

      if (entidade.Id == null)
      {
        dbSet.Add(entidade);
      }
      else
      {
        dbSet.Attach(entidade);
        _dbContext.Entry(entidade).State = EntityState.Modified;
      }

      _dbContext.SaveChanges();
    }
  }
}