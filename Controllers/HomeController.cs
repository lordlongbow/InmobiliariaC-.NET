using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using royectoInmobiliaria.net_MVC_.Models;

namespace royectoInmobiliaria.net_MVC_.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private InmuebleRepositorio InmuebleRepositorio = new InmuebleRepositorio();
    private ContratoReositorio ContratoReositorio = new ContratoReositorio();

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult CargarVistasMenuLateral(string opcionMenu){
        switch(opcionMenu){
            case "_inmueblesDisponibles": 
            {
                    var inmueblesLibres = InmuebleRepositorio.verLibres();

                    return PartialView("_inmueblesDisponibles", inmueblesLibres);
            }
            case "_contratosVigentes":
            {
                var contratoVigente = ContratoReositorio.ContratosVigentes();
                return PartialView("_contratosVigentes", contratoVigente);
            }
            default: {
                return View();
            }
        }
    }
}

