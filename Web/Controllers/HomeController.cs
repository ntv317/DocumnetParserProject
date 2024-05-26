using System.Diagnostics;
using Database.ModelFilter;
using Microsoft.AspNetCore.Mvc;
using DocumnetParserProject.Models;
using Services.Common.Interfaces;
using Services.TransactionServices.Interfaces;

namespace DocumnetParserProject.Controllers;

public class HomeController : Controller
{
    private readonly ITransactionService _transactionService;
    
    public HomeController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
        
    }
    public IActionResult Index()
    {
       
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Upload( IFormFile file)
    {
        await _transactionService.ProcessData(file);
        return Ok();
    }
    public async Task<JsonResult> GetList(OperationFilter filter)
    {
        var res = await _transactionService.GetList(filter);
        return Json(res);
    }
    
    public async Task<IActionResult> Operation()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}