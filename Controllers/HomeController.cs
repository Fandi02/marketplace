using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using marketplace.Models;
using marketplace.ViewModels;
using Microsoft.EntityFrameworkCore;
using marketplace.Datas.Entities;

namespace marketplace.Controllers;

public class HomeController : Controller
{
    private readonly marketplace.Datas.marketplaceContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, marketplace.Datas.marketplaceContext context)
    {
        _logger = logger;
        _context = context;
    }

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

    public IActionResult TambahProduk()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> TambahProduk(Produk tambahProduk)
    {
        if (ModelState.IsValid)
        {
            _context.Add(tambahProduk);
            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }
        return View(tambahProduk);
    }

    public IActionResult TambahKategori()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> TambahKategori(KategoriProduk tambahKategori)
    {
        if (ModelState.IsValid)
        {
            _context.Add(tambahKategori);
            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }
        return View(tambahKategori);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
