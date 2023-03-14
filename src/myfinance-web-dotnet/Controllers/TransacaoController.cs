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
    public TransacaoController(
      ILogger<TransacaoController> logger,
      ITransacaoService transacaoService,
      IPlanoContaService planoContaService
      )
    {
      _logger = logger;
      _transacaoService = transacaoService;
      _planoContaService = planoContaService;
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

      model.PlanoContas = new SelectList(listaPlanoContas, "Id", "Descricao");

      return View(model);
    }

    [HttpPost]
    [Route("Cadastro")]
    [Route("Cadastro/{id}")]
    public IActionResult Cadastro(TransacaoModel model)
    {
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