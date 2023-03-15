using Microsoft.AspNetCore.Mvc;
using myfinance_web_dotnet.Models;
using myfinance_web_dotnet.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace myfinance_web_dotnet.Controllers
{
  [Route("[controller]")]
  public class TransacaoController : Controller
  {
    private readonly ILogger<TransacaoController> _logger;
    private readonly ITransacaoService _transacaoService;
    private readonly IPlanoContaService _planoContaService;
    private readonly IMetodoPagamentoService _metodoPagamentoService;
    public TransacaoController(
      ILogger<TransacaoController> logger,
      ITransacaoService transacaoService,
      IPlanoContaService planoContaService,
      IMetodoPagamentoService metodoPagamentoService
      )
    {
      _logger = logger;
      _transacaoService = transacaoService;
      _planoContaService = planoContaService;
      _metodoPagamentoService = metodoPagamentoService;
    }

    [HttpGet]
    [Route("Index")]
    public IActionResult Index()
    {
      ViewBag.ListaTransacao = _transacaoService.ListarTransacoes();
      return View();
    }

    [HttpGet]
    [Route("Cadastro")]
    [Route("Cadastro/{id}")]
    public IActionResult Cadastro(int? id)
    {
      var model = new TransacaoModel();
      if (id != null)
      {
        model = _transacaoService.RetornarTransacao((int)id);
      }
      else
      {
        model.Data = DateTime.Now;
      }
      var listaPlanoContas = _planoContaService.ListarRegistros();
      var listaMetodosPagamento = _metodoPagamentoService.ListarMetodos();

      model.PlanoContas = new SelectList(listaPlanoContas, "Id", "Descricao");
      model.MetodosPagamento = new SelectList(listaMetodosPagamento, "Id", "Tipo");

      return View(model);
    }

    [HttpPost]
    [Route("Cadastro")]
    [Route("Cadastro/{id}")]
    public IActionResult Cadastro(TransacaoModel model)
    {
      var transaction = model;

      if (model.PlanoContaId != null)
      {
        var planoContaTipo = _planoContaService.RetornarRegistro(model.PlanoContaId).Tipo;

        var receita = "R";
        if (planoContaTipo.ToString() == receita.ToString())
        {
          model.MetodoPagamentoId = null;
          model.ItemMetodoPagamento = null;
        }
      }
      _transacaoService.Salvar(model);
      return RedirectToAction("Index");
    }

    [HttpGet]
    [Route("Excluir/{id}")]
    public IActionResult Excluir(int id)
    {
      _transacaoService.Excluir(id);
      return RedirectToAction("Index");
    }
  }
}