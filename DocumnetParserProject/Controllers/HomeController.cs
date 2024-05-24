using System.Diagnostics;
using Database.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using DocumnetParserProject.Models;
using Services.ParserServices.Interfaces;

namespace DocumnetParserProject.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IParserFactory _parserFactory;

    public HomeController(ILogger<HomeController> logger, IParserFactory parserFactory)
    {
        _logger = logger;
        _parserFactory = parserFactory;
    }

    public IActionResult Index()
    {
        var service = _parserFactory.GetParserService(FileType.Csv);
       
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
}