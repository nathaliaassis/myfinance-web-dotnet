using Microsoft.AspNetCore.Mvc;
using myfinance_web_dotnet.Domain.Services.Interfaces;
using myfinance_web_dotnet.Models;

namespace myfinance_web_dotnet.Controllers
{
  [Route("[controller]")]
  public class PlanoContaController : Controller
  {
    private readonly ILogger<PlanoContaController> _logger;
    private readonly IPlanoContaService _planoContaService;
    public PlanoContaController(
      ILogger<PlanoContaController> logger,
      IPlanoContaService planoContaService
      )
    {
      _logger = logger;
      _planoContaService = planoContaService;
    }

    [HttpGet]
    [Route("Index")]
    public IActionResult Index()
    {
      ViewBag.ListaPlanoConta = _planoContaService.ListarRegistros();
      return View();
    }

    [HttpGet]
    [Route("Cadastro")]
    [Route("Cadastro/{id}")]
    public IActionResult Cadastro(int? id)
    {
      if (id != null)
      {
        var registro = _planoContaService.RetornarRegistro((int)id);
        return View(registro);
      }
      return View();
    }

    [HttpPost]
    [Route("Cadastro")]
    [Route("Cadastro/{id}")]
    public IActionResult Cadastro(PlanoContaModel model)
    {
      _planoContaService.Salvar(model);
      return RedirectToAction("Index");
    }

    [HttpGet]
    [Route("Excluir/{id}")]
    public IActionResult Excluir(int id)
    {
      _planoContaService.Excluir(id);
      return RedirectToAction("Index");
    }
  }
}