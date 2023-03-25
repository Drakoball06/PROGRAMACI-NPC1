using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PC1YOSSEF.Models;

namespace PC1YOSSEF.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

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

    public IActionResult Tarjeta()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Calculadora(string numeroTarjeta, DateTime fechaVencimiento, decimal montoPagar)
    {
        decimal mora = 0.00m;

        // Calculamos la mora si la fecha de vencimiento ya pasó
        if (fechaVencimiento < DateTime.Today)
        {
            int diasRetraso = (int)(DateTime.Today - fechaVencimiento).TotalDays;
            mora = montoPagar * (decimal)(diasRetraso * 0.00005);
        }

        decimal totalPagar = montoPagar + mora;

        ViewBag.NumeroTarjeta = numeroTarjeta;
        ViewBag.FechaVencimiento = fechaVencimiento.ToShortDateString();
        ViewBag.MontoPagar = montoPagar;
        ViewBag.Mora = mora;
        ViewBag.TotalPagar = totalPagar;
        
        System.Console.WriteLine(totalPagar);
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
