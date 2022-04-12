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
public class KeranjangController : BaseController
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

    public async Task<IActionResult> Index(){
        var idPembeli = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt();
        var result = await _keranjangService.Get(idPembeli);
        var alamat = await _accountService.GetAlamat(idPembeli);

        ViewBag.AlamatList = alamat.Select(x=> new SelectListItem(x.Item2.ToString(), x.Item1.ToString())).ToList();
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
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int?  id)
    {
        if(id == null)
        {
            return Json(new
            {
                success = false,
                message = "keranjang item yang mau dihapus tidak ditemukan"
            });
        }

        try
        {

            await _keranjangService.Delete(id.Value);

            return Json(new
            {
                success = true
            });
        }
        catch (InvalidOperationException ex)
        {
            return Json(new
            {
                success = false,
                message = ex.Message
            });
        }
        catch
        {
            throw;
        }
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(KeranjangUpdateViewModel request)
    {

        if (!ModelState.IsValid)
        {
            return Json(new
            {
                success = false,
                message = "bad request"
            });
        }

        try
        {

            await _keranjangService.Update(new Datas.Entities.Keranjang
            {
                IdKeranjang = request.Id,
                JmlBarang = request.JmlBarang,
                IdPembeli = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt()
            });

            return Json(new
            {
                success = true
            });
        }
        catch (InvalidOperationException ex)
        {
            return Json(new
            {
                success = false,
                message = ex.Message
            });
        }
        catch
        {
            throw;
        }
    }
}