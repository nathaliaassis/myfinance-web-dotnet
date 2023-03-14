using Microsoft.EntityFrameworkCore;
using myfinance_web_dotnet.Domain.Entities;
using myfinance_web_dotnet.Domain.Services.Interfaces;
using myfinance_web_dotnet.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace myfinance_web_dotnet.Domain.Services
{
  public class TransacaoService : ITransacaoService
  {
    private readonly MyFinanceDbContext _dbContext;

    public TransacaoService(
      MyFinanceDbContext dbContext
    )
    {
      _dbContext = dbContext;
    }
    public void Excluir(int id)
    {

      var dbSet = _dbContext.Transacao;
      var item = dbSet.Where(x => x.Id == id).First();

      _dbContext.Attach(item);
      _dbContext.Remove(item);
      _dbContext.SaveChanges();
    }
    public List<TransacaoModel> ListarTransacoes()
    {
      var result = new List<TransacaoModel>();
      var dbSet = _dbContext.Transacao.Include(x => x.PlanoConta).Include(x => x.MetodoPagamento);

      foreach (var item in dbSet)
      {
        var itemTransacao = new TransacaoModel()
        {
          Id = item.Id,
          Data = item.Data,
          Historico = item.Historico,
          Valor = item.Valor,
          ItemPlanoConta = new PlanoContaModel()
          {
            Id = item.PlanoConta.Id,
            Descricao = item.PlanoConta.Descricao,
            Tipo = item.PlanoConta.Tipo
          },
          PlanoContaId = item.PlanoContaId,
          MetodoPagamentoId = item.MetodoPagamentoId
        };

        if (item.MetodoPagamentoId != null)
        {
          itemTransacao.ItemMetodoPagamento = new MetodoPagamentoModel()
          {
            Id = item.MetodoPagamento?.Id,
            Tipo = item.MetodoPagamento?.Tipo
          };
        }

        result.Add(itemTransacao);
      }

      return result;
    }

    public TransacaoModel RetornarTransacao(int id)
    {
      var item = _dbContext.Transacao.Where(x => x.Id == id).First();

      var itemTransacao = new TransacaoModel()
      {
        Id = item.Id,
        Data = item.Data,
        Historico = item.Historico,
        Valor = item.Valor,
        PlanoContaId = item.PlanoContaId,
        MetodoPagamentoId = item.MetodoPagamentoId
      };

      return itemTransacao;
    }

    public void Salvar(TransacaoModel model)
    {
      var dbSet = _dbContext.Transacao;

      var entidade = new Transacao()
      {
        Id = model.Id,
        Data = model.Data,
        Historico = model.Historico,
        Valor = model.Valor,
        PlanoContaId = model.PlanoContaId,
        MetodoPagamentoId = model.MetodoPagamentoId,
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