using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using marketplace.Models;
using marketplace.ViewModels;
using Microsoft.EntityFrameworkCore;
using marketplace.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace marketplace.Controllers;


[Authorize(Roles = AppConstant.ADMIN_ROLE)]
public class AdminController : Controller
{
    private readonly IAdminService _adminService;
    private readonly ILogger<AdminController> _logger;

    public AdminController(ILogger<AdminController> logger, IAdminService adminService)
    {
        _logger = logger;
        _adminService = adminService;
    }

    public async Task<IActionResult> Index()
    {
        var dbResult = await _adminService.GetAll();

        var viewModels = new List<AdminViewModel>();

        for (int i = 0; i < dbResult.Count; i++)
        {
            viewModels.Add(new AdminViewModel
            {
                IdAdmin = dbResult[i].IdAdmin,
                Nama = dbResult[i].Nama,
                NoHp = dbResult[i].NoHp,
                Username = dbResult[i].Username,
                Password = dbResult[i].Password,
                Email = dbResult[i].Email,
            });
        }

        return View(viewModels);
    }

    public IActionResult Create()
    {
        return View(new AdminViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AdminViewModel request)
    {
        if (!ModelState.IsValid)
        {
            return View(request);
        }
        try
        {
            await _adminService.Add(request.ConvertToDbModel());

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
            return BadRequest();
        }

        var result = await _adminService.Get(id.Value);

        if (result == null)
        {
            return NotFound();
        }
        return View(new AdminViewModel(result));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, AdminViewModel request)
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
            await _adminService.Update(request.ConvertToDbModel());
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
        return View(request);
    }


    public async Task<IActionResult> Delete(int? id){
        if(id == null)
        {
            return BadRequest();
        }

        var result = await _adminService.Get(id.Value);

        if(result == null) {
            return NotFound();
        }

        return View(new AdminViewModel(result));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int? id, AdminViewModel request)
    {
        if(id == null) {
            return BadRequest();
        }
        
        try{
            await _adminService.Delete(id.Value);

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