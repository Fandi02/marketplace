using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using marketplace.Models;
using marketplace.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;
using marketplace.Interfaces;
using System.Security.Claims;
using marketplace.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace marketplace.Controllers;

[Authorize(Roles = AppConstant.CUSTOMER_ROLE)]
public class KeranjangController : Controller
{
    private readonly ILogger<KeranjangController> _logger;
    private readonly IKeranjangService _keranjangService;
    private readonly IAccountService _accountService;
    public KeranjangController(ILogger<KeranjangController> logger, IKeranjangService keranjangService, IAccountService accountService)
    {
        _logger = logger;
        _keranjangService = keranjangService;
        _accountService = accountService;
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if(HttpContext.User == null || HttpContext.User.Identity == null){
            ViewBag.IsLogged = false;
        } else {
            ViewBag.IsLogged = HttpContext.User.Identity.IsAuthenticated;
        }

        base.OnActionExecuted(context);
    }

    public async Task<IActionResult> Index(){
        var idPembeli = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt();
        var result = await _keranjangService.Get(idPembeli);
        var alamat = await _accountService.GetAlamat(idPembeli);
        return View(result);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(int? produkId)
    {
        if(produkId == null)
        {
            return BadRequest();
        }

        await _keranjangService.Add(new Datas.Entities.Keranjang
        {
            IdProduk = produkId.Value,
            JmlBarang = 1,
            IdPembeli = HttpContext.User.Claims.FirstOrDefault(x=>x.Type == ClaimTypes.NameIdentifier).Value.ToInt()
        });

        return RedirectToAction(nameof(Index));
    }
}