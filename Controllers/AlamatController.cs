using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using marketplace.Models;
using marketplace.ViewModels;
using Microsoft.EntityFrameworkCore;
using marketplace.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace marketplace.Controllers;

[Authorize (Roles = AppConstant.CUSTOMER_ROLE)]
public class AlamatController : Controller
{
    private readonly IAlamatService _alamatService;
    private readonly ILogger<AlamatController> _logger;

    public AlamatController(ILogger<AlamatController> logger, IAlamatService alamatService)
    {
        _logger = logger;
        _alamatService = alamatService;
    }

    public async Task<IActionResult> Index()
    {
        var dbResult = await _alamatService.GetAll();

        var viewModels = new List<AlamatViewModel>();

        for (int i = 0; i < dbResult.Count; i++)
        {
            viewModels.Add(new AlamatViewModel
            {
                IdAlamat = dbResult[i].IdAlamat,
                Kota = dbResult[i].Kota,
                Kecamatan = dbResult[i].Kecamatan,
                Desa = dbResult[i].Desa,
                KodePos = dbResult[i].KodePos,
                Deskripsi = dbResult[i].Deskripsi,
            });
        }

        return View(viewModels);
    }

    public IActionResult Create()
    {
        return View(new AlamatViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AlamatViewModel request)
    {
        if (!ModelState.IsValid)
        {
            return View(request);
        }
        try
        {
            await _alamatService.Add(request.ConvertToDbModel());

            return Redirect(nameof(Index));
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


   public async Task<IActionResult> Edit(int? id)
     {
          if (id == null)
          {
               return new NotFoundResult();
          }

          var data = await _alamatService.Get(id.Value);
          if (data == null)
          {
               return NotFound();
          }

          return View(new AlamatViewModel()
          {
              Provinsi = data.Provinsi,
                Kota = data.Kota,
                Kecamatan = data.Kecamatan,
                Desa = data.Desa,
                KodePos = data.KodePos,
                Deskripsi = data.Deskripsi,
          });
     }

    [HttpPost]
     [ValidateAntiForgeryToken]
     public async Task<IActionResult> Edit(int? id, AlamatViewModel request)
     {
          if (id == null)
          {
               return BadRequest();
          }
          if (!ModelState.IsValid)
          {
               return View(request);
          }
          try
          {
               request.IdAlamat = id.Value;
               var updated = await _alamatService.Update(request.ConvertToDbModel());
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int? id, AlamatViewModel request)
    {
        if(id == null) {
            return BadRequest();
        }
        
        try{
            await _alamatService.Delete(id.Value);

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