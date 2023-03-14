namespace myfinance_web_dotnet.Domain.Entities
{
  public class Transacao
  {
    public int? Id { get; set; }
    public DateTime Data { get; set; }
    public decimal Valor { get; set; }
    public string? Historico { get; set; }
    public int PlanoContaId { get; set; }
    public int? MetodoPagamentoId { get; set; }
    public MetodoPagamento? MetodoPagamento { get; set; }

    public PlanoConta PlanoConta { get; set; }
  }
}