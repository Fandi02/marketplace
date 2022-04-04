using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using marketplace.Models;
using marketplace.ViewModels;
using Microsoft.EntityFrameworkCore;
using marketplace.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using marketplace.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace marketplace.Controllers;

[Authorize(Roles = AppConstant.ADMIN_ROLE)]
public class ProdukController : Controller
{
    private readonly IProdukService _produkService;
    private readonly IKategoriService _kategoriService;
    private readonly IProdukKategoriService _produkKategoriService;
    private readonly ILogger<HomeController> _logger;
    private readonly IWebHostEnvironment _iwebHost;
    public ProdukController(IProdukService produkService,
    ILogger<HomeController> logger,
    IKategoriService kategoriService,
    IWebHostEnvironment iwebHost,
    IProdukKategoriService produkKategoriService)
    {
        _produkService = produkService;
        _iwebHost = iwebHost;
        _logger = logger;
        _kategoriService = kategoriService;
        _produkKategoriService = produkKategoriService;
    }

    [Route("produk/index")]
    public async Task<IActionResult> Index()
    {
        var dbResult = await _produkService.GetAll();
        var ViewModels = new List<ProdukViewModel>();
        for (int i = 0; i < dbResult.Count; i++)
        {
            ViewModels.Add(new ProdukViewModel
            {
                IdProduk = dbResult[i].IdProduk,
                Nama = dbResult[i].Nama,
                Deskripsi = dbResult[i].Deskripsi,
                Harga = dbResult[i].Harga,
                Stok = dbResult[i].Stok,
                Gambar = dbResult[i].Gambar,
                Kategories = dbResult[i].ProdukKategoris.Select(x => new KategoriViewModel
                {
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

        ViewBag.KategoriDataSource = KategoriViewModels
        .Select(x => new SelectListItem
        {
            Value = x.IdKategori.ToString(),
            Text = x.Nama,
            Selected = false
        }).ToList();
    }

    private async Task SetKategoriDataSource(int[] kategoris)
    {
        if (kategoris == null)
        {
            await SetKategoriDataSource();
            return;
        }

        var KategoriViewModels = await _kategoriService.GetAll();

        ViewBag.KategoriDataSource = KategoriViewModels
        .Select(x => new SelectListItem
        {
            Value = x.IdKategori.ToString(),
            Text = x.Nama,
            Selected = kategoris.FirstOrDefault(y => y == x.IdKategori) == 0 ? false : true
        }).ToList();
    }
    public async Task<IActionResult> Create()
    {
        await SetKategoriDataSource();
        return View(new ProdukViewModel());
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return BadRequest();
        }

        var produk = await _produkService.Get(id.Value);

        if (produk == null)
        {
            return NotFound();
        }
        var kategoriIds = await _produkKategoriService.GetKategoriIds(id.Value);
        await SetKategoriDataSource(kategoriIds);
        return View(new ProdukViewModel()
        {
            IdProduk = produk.IdProduk,
            Nama = produk.Nama,
            Deskripsi = produk.Deskripsi,
            Harga = produk.Harga,
            Stok = produk.Stok,
            Gambar = produk.Gambar,
            KategoriId = kategoriIds
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, ProdukViewModel request)
    {
        if (id == null)
        {
            await SetKategoriDataSource();
            return View(request);
            // return BadRequest();
        }
        if (!ModelState.IsValid)
        {
            await SetKategoriDataSource(request.KategoriId);
            return View(request);
        }
        try
        {
            string fileName = string.Empty;
            if (request.GambarFile != null)
            {
                fileName = $"{Guid.NewGuid()}-{request.GambarFile?.FileName}";
                string filePathName = _iwebHost.WebRootPath + "\\images\\" + fileName;

                using (var streamWriter = System.IO.File.Create(filePathName))
                {
                    await streamWriter.WriteAsync(request.GambarFile.OpenReadStream().ToBytes());
                }
            }
            var produk = request.ConvertToDbModel();
            if (request.GambarFile != null)
            {
                produk.Gambar = $"/images/{fileName}";
            }

            //Update ProdukKategori
            var ProdukKategoris = await _produkKategoriService.GetKategoriIds(request.IdProduk);

            for (int i = 0; i < request.KategoriId.Length; i++)
            {
                if (ProdukKategoris != null && ProdukKategoris.Any())
                {
                    if (!ProdukKategoris.Any(x => x == request.KategoriId[i]))
                    {
                        produk.ProdukKategoris.Add(new Datas.Entities.ProdukKategori
                        {
                            IdKategori = request.KategoriId[i],
                            IdProduk = produk.IdProduk
                        });
                    }
                }
                else
                {
                    produk.ProdukKategoris.Add(new Datas.Entities.ProdukKategori
                    {
                        IdKategori = request.KategoriId[i],
                        IdProduk = produk.IdProduk
                    });
                }
            }

            //Remove kategorid yang tidak dipilih
            if (ProdukKategoris != null && (produk.ProdukKategoris != null && produk.ProdukKategoris.Any()))
            {
                foreach (var item in ProdukKategoris)
                {
                    if (!produk.ProdukKategoris.Any(x => x.IdKategori == item))
                    {
                        await _produkKategoriService.Remove(request.IdProduk, item);
                    }
                }
            }
            await _produkService.Update(produk);
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {

            ViewBag.ErrorMessage = ex.Message;
        }
        catch
        {
            throw;
        }
        await SetKategoriDataSource(request.KategoriId);
        return View(request);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProdukViewModel request)
    {
        if (!ModelState.IsValid)
        {
            await SetKategoriDataSource();
            return View(request);
        }
        if (request == null)
        {
            await SetKategoriDataSource();
            return View(request);
        }

        try
        {
            string fileName = string.Empty;

            if (request.GambarFile != null)
            {
                fileName = $"{Guid.NewGuid()}-{request.GambarFile?.FileName}";

                string filePathName = _iwebHost.WebRootPath + $"/images/{fileName}";

                using (var streamWriter = System.IO.File.Create(filePathName))
                {
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
        catch (Exception)
        {
            throw;
        }
        await SetKategoriDataSource();
        return View(request);
    }





    public async Task<IActionResult> Delete(int? Id)
    {
        if (Id == null)
        {
            return BadRequest();
        }

        var result = await _produkService.Get(Id.Value);

        if (result == null)
        {
            return NotFound();
        }

        return View(new ProdukViewModel(result));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int? Id, ProdukViewModel request)
    {
        if (Id == null)
        {
            return BadRequest();
        }

        try
        {
            await _produkService.Delete(Id.Value);

            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            ViewBag.ErrorMessage = ex.Message;
        }
        catch (Exception)
        {
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