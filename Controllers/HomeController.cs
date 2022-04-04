using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using marketplace.Models;
using marketplace.ViewModels;
using Microsoft.EntityFrameworkCore;
using marketplace.Datas.Entities;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authentication;
using marketplace.Interfaces;

namespace marketplace.Controllers;

public class HomeController : Controller
{
    private readonly marketplace.Datas.marketplaceContext _context;
    private readonly ILogger<HomeController> _logger;
    private readonly IProdukService _produkService;

    public HomeController(ILogger<HomeController> logger, marketplace.Datas.marketplaceContext context,
    IProdukService produkService)
    {
        _logger = logger;
        _context = context;
        _produkService = produkService;
    }

    // ?????????????????????
    public async Task<IActionResult> Index()
    {
        var dbResult = await _context.Produks.Select(x => new ProdukViewModel
        {
            IdProduk = x.IdProduk,
            Nama = x.Nama,
            Deskripsi = x.Deskripsi,
            Harga = x.Harga,
            Stok = x.Stok,
            Gambar = x.Gambar
        }).ToListAsync();
        return View(dbResult);
    }

    public async Task<IActionResult> Detail(int?id){
        if(id == null){
            return NotFound();
        }
        var produk = await _produkService.Get(id.Value);
        if(produk == null){
            return NotFound();
        }

        return View(new ProdukViewModel(){
            IdProduk = produk.IdProduk,
            Nama = produk.Nama,
            Deskripsi = produk.Deskripsi,
            Harga = produk.Harga,
            Stok = produk.Stok,
            Gambar = produk.Gambar
        });
    }

    public IActionResult Denied(){
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult Masuk()
    {
        return View();
    }
    public IActionResult Daftar()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Daftar(Pembeli daftar)
    {
        if (ModelState.IsValid)
        {
            _context.Add(daftar);
            await _context.SaveChangesAsync();
            return RedirectToAction("Masuk");
        }
        return View(daftar);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
