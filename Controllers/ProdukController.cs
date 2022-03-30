using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using marketplace.Models;
using marketplace.ViewModels;
using Microsoft.EntityFrameworkCore;
using marketplace.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using marketplace.Helpers;

namespace marketplace.Controllers;

public class ProdukController : Controller
{
    private readonly IProdukService _produkService;
    private readonly IKategoriService _kategoriService;
    private readonly ILogger<HomeController> _logger;
    private readonly IWebHostEnvironment _iwebHost;
    public ProdukController(IProdukService produkService, ILogger<HomeController> logger, IKategoriService kategoriService, IWebHostEnvironment iwebHost)
    {
        _produkService = produkService;
        _iwebHost = iwebHost;
        _logger = logger;
        _kategoriService = kategoriService;
    }

    public async Task<IActionResult> Index()
    {
        var dbResult = await _produkService.GetAll();
        var ViewModels = new List<ProdukViewModel>();
        for (int i = 0; i < dbResult.Count; i++)
        {
            ViewModels.Add(new ProdukViewModel{
                IdProduk = dbResult[i].IdProduk,
                Nama = dbResult[i].Nama,
                Deskripsi = dbResult[i].Deskripsi,
                Harga = dbResult[i].Harga,
                Stok = dbResult[i].Stok,
                Gambar = dbResult[i].Gambar,
                Kategories = dbResult[i].ProdukKategoris.Select(x => new KategoriViewModel {
                    Id = x.IdKategoriNavigation.IdKategori,
                    Nama = x.IdKategoriNavigation.Nama,
                    Icon = x.IdKategoriNavigation.Icon
                }).ToList()

            });
        }
        return View(ViewModels);
    }
    private async Task SetKategoriDataSource()
    {
        var KategoriViewModels = await _kategoriService.GetAll();

        ViewBag.KategoriDataSource = KategoriViewModels.Select(x => new SelectListItem
        {
            Value = x.IdKategori.ToString(),
            Text = x.Nama,
            Selected = false
        }).ToList();
    }
    public async Task<IActionResult> Create()
    {
        await SetKategoriDataSource();
        return View(new ProdukViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProdukViewModel request)
    {
        if (!ModelState.IsValid)
        {
            await SetKategoriDataSource();
            return View(request);
        }
        if (request == null){
            await SetKategoriDataSource();
            return View(request);
        }

        try
        {
            string fileName = string.Empty;
            
            if(request.GambarFile != null) 
            {
                fileName = $"{Guid.NewGuid()}-{request.GambarFile?.FileName}";

                string filePathName = _iwebHost.WebRootPath + $"/images/{fileName}";
                
                using(var streamWriter = System.IO.File.Create(filePathName)){
                    //await streamWriter.WriteAsync(Common.StreamToBytes(request.GambarFile.OpenReadStream()));
                    //using extension to convert stream to bytes
                    await streamWriter.WriteAsync(request.GambarFile.OpenReadStream().ToBytes());
                }
            }

            var produk = request.ConvertToDbModel();
            produk.Gambar = $"images/{fileName}";

            //Insert to ProdukKategori table
            for (int i = 0; i < request.KategoriId.Length; i++)
            { 
                produk.ProdukKategoris.Add(new Datas.Entities.ProdukKategori 
                {
                    IdKategori = request.KategoriId[i],
                    IdProduk = produk.IdProduk
                });   
            }

            await _produkService.Add(produk);

            return Redirect(nameof(Index));

            // var Produk = request.ConvertToDbModel();
            // Produk.ProdukKategoris.Add(new Datas.Entities.ProdukKategori{
            //     IdKategori = request.KategoriId,
            //     IdProduk = Produk.IdProduk
            // });
            // await _produkService.Add(request.ConvertToDbModel());
            // return View(request);
        }
        catch (InvalidOperationException ex)
        {
            ViewBag.ErrorMessage = ex.Message;
        }
        catch(Exception)
        {
            throw;
        }
        await SetKategoriDataSource();
        return View(request);
    }

    // public async Task<IActionResult> Edit(int? id)
    // {
    //     if (id == null)
    //     {
    //         return BadRequest();
    //     }

    //     var result = await _produkService.Get(id.Value);

    //     if (result == null)
    //     {
    //         return NotFound();
    //     }
    //     return View(new ProdukViewModel(result));
    // }

    // [HttpPost]
    // [ValidateAntiForgeryToken]
    // public async Task<IActionResult> Edit(int? id, ProdukViewModel request)
    // {
    //     if (id == null)
    //     {
    //         return BadRequest();
    //     }
    //     if (!ModelState.IsValid)
    //     {
    //         return View(request);
    //     }
    //     try
    //     {
    //         await _produkService.Update(request.ConvertToDbModel());
    //         return RedirectToAction(nameof(Index));
    //     }
    //     catch (InvalidOperationException ex)
    //     {

    //         ViewBag.ErrorMessage = ex.Message;
    //     }
    //     catch
    //     {
    //         throw;
    //     }
    //     return View(request);
    // }



    public async Task<IActionResult> Delete(int? Id){
        if(Id == null)
        {
            return BadRequest();
        }

        var result = await _produkService.Get(Id.Value);

        if(result == null) {
            return NotFound();
        }

        return View(new ProdukViewModel(result));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int? Id, ProdukViewModel request)
    {
        if(Id == null) {
            return BadRequest();
        }
        
        try{
            await _produkService.Delete(Id.Value);

            return RedirectToAction(nameof(Index));  
        }catch(InvalidOperationException ex){
            ViewBag.ErrorMessage = ex.Message;
        }
        catch(Exception) {
            throw;
        }

        return View(request);
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