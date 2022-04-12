using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using marketplace.Models;
using marketplace.ViewModels;
using Microsoft.EntityFrameworkCore;
using marketplace.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using marketplace.Helpers;

namespace marketplace.Controllers;

[Authorize (Roles = AppConstant.CUSTOMER_ROLE)]
public class DetailOrderController : Controller
{
    private readonly IDetailOrderService _detailOrderService;
    private readonly ILogger<DetailOrderController> _logger;

    public DetailOrderController(ILogger<DetailOrderController> logger, IDetailOrderService detailOrderService)
    {
        _logger = logger;
        _detailOrderService = detailOrderService;
    }

    public async Task<IActionResult> DetailOrder()
    {
        var id = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt();
        // var result = await _detailOrderService.Get(id);
        return View(id);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}