using myfinance_web_dotnet.Models;

namespace myfinance_web_dotnet.Domain.Services.Interfaces
{
  public interface ITransacaoService
  {
    List<TransacaoModel> ListarTransacoes();
    void Salvar(TransacaoModel model);
    TransacaoModel RetornarTransacao(int id);
    void Excluir(int id);
  }
}