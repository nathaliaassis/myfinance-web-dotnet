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
    public IActionResult Cadastro()
    {
      return View();
    }

    [HttpPost]
    [Route("Cadastro")]
    public IActionResult Cadastro(PlanoContaModel model)
    {
      return RedirectToAction("Index");
    }
  }
}