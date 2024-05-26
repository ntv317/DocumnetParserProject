using Microsoft.AspNetCore.Mvc;
using Services.Common.Interfaces;

namespace DocumnetParserProject.Controllers;

public class DropdownListController:Controller
{
    private readonly ICurrencyService _currencyService;

    public DropdownListController(ICurrencyService currencyService)
    {
        _currencyService = currencyService;
    }

    public JsonResult CurrencyList(string term)
    {
        return Json(_currencyService.GetDropdownList(term));
    }
}