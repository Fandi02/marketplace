using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using marketplace.Models;
using marketplace.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Filters;
using marketplace.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using marketplace.Helpers;
using marketplace.Datas.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Filters;

namespace marketplace.Controllers;

[Authorize]
public class OrderController : BaseController
{
    private readonly ILogger<OrderController> _logger;
    private readonly IKeranjangService _keranjangService;
    private readonly IOrderService _orderService;
    private readonly IStatusService _statusService;
    private readonly IWebHostEnvironment _iWebHost;

    public OrderController(ILogger<OrderController> logger, 
    IKeranjangService keranjangService, 
    IOrderService orderService, 
    IStatusService statusService,
    IWebHostEnvironment iWebHost)
    {
        _logger = logger;
        _keranjangService = keranjangService;
        _orderService = orderService;
        _statusService = statusService;
        _iWebHost = iWebHost;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);
    }
    [Authorize(Roles = AppConstant.ADMIN_ROLE)]
    public async Task<IActionResult> Index(int? page, int? pageCount)
    {
        if (HttpContext.User == null || HttpContext.User.Identity == null)
        {
            ViewBag.IsLogged = false;
        }

        var tuplePagination = Common.ToLimitOffset(page, pageCount);

        var result = await _orderService.GetV2(tuplePagination.Item1, tuplePagination.Item2);

        await SetStatusListAsSelectListItem();
        ViewBag.FilterDate = null;

        return View(result);
    }

    [Authorize(Roles = AppConstant.ADMIN_ROLE)]
    public async Task<IActionResult> Detail(int? id)
    {

        if (id == null)
        {
            return BadRequest();
        }

        var result = await _orderService.GetDetailAdmin(id.Value);

        return View(result);
    }

    public async Task<IActionResult> Kirim(KirimRequestViewModel request){
        if(!ModelState.IsValid)
        {
            return BadRequest();
        }

        Pengiriman dataPengiriman = new Pengiriman
        {
            IdOrder = request.IdOrder,
            IdAlamat = request.IdAlamat,
            Kurir = request.Kurir,
            NoResi = string.Empty,
            Ongkir = (int)request.Ongkir,
            Status = string.Empty,
            Keterangan = string.Empty
        };

        await _orderService.Kirim(dataPengiriman);

        await _orderService.UpdateStatus(dataPengiriman.IdOrder, AppConstant.StatusOrder.DIKIRIM);

        return RedirectToAction(nameof(Detail), new {
            id = request.IdOrder
        });
    }

    [HttpPost]
    public async Task<IActionResult> Index([FromQuery] int? page, [FromQuery] int? pageCount, int? status, DateTime? date)
    {
        var tuplePagination = Common.ToLimitOffset(page, pageCount);

        var result = await _orderService.GetV2(tuplePagination.Item1, tuplePagination.Item2, status, date);

        await SetStatusListAsSelectListItem(status);
        if (date != null)
        {
            ViewBag.FilterDate = date.Value.ToString("MM/dd/yyyy");
        }

        return View(result);
    }

    private async Task SetStatusListAsSelectListItem(int? status = null)
    {
        var statusList = await _statusService.Get();

        if (statusList == null || !statusList.Any())
        {
            ViewBag.StatusList = new List<SelectListItem>();
        }
        else
        {
            ViewBag.StatusList = statusList.Select(x => new SelectListItem
            {
                Value = x.IdStatusOrder.ToString(),
                Text = x.Nama,
                Selected = status != null && status.Value == x.IdStatusOrder
            }).ToList();
        }
    }

    [Authorize(Roles = AppConstant.CUSTOMER_ROLE)]
    public async Task<IActionResult> OrderUser()
    {
        var result = await _orderService.Get(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt());

        return View(result);
    }

    [Authorize(Roles = AppConstant.CUSTOMER_ROLE)]
    public async Task<IActionResult> OrderUserDetail(int? id)
    {

        if (id == null)
        {
            return BadRequest();
        }

        var result = await _orderService.GetDetail(id.Value, HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt());

        return View(result);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Checkout(CheckoutViewModel? request)
    {
        if (request == null)
        {
            return BadRequest();
        }
        // if (request.Alamat == 0)
        // {
        //     return BadRequest();
        // }

        int idPembeli = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt();
        var result = await _keranjangService.Get(idPembeli);

        if (result == null || !result.Any())
        {
            return BadRequest();
        }
        foreach (var item in result)
        {
            int keranjangId = request.Id.FirstOrDefault(x => item.Id == x);

            if (keranjangId < 1)
            {
                continue;
            }
            int jumlahBarangBaru = request.Qty[Array.IndexOf(request.Id, keranjangId)];

            item.JmlBarang = jumlahBarangBaru;
            item.Subtotal = item.Harga * jumlahBarangBaru;
        }

        var newOrder = new Order();

        newOrder.IdPembeli = idPembeli;
        newOrder.JmlBayar = result.Sum(x => x.Subtotal);
        newOrder.Notes = string.Empty;
        // newOrder.Status = 1;
        newOrder.IdAlamat = 1;
        newOrder.TglTransaksi = DateTime.Now;
        newOrder.IdStatusOrder = 1;
        newOrder.DetailOrders = new List<DetailOrder>();

        foreach (var item in result)
        {
            newOrder.DetailOrders.Add(new DetailOrder
            {
                IdOrder = newOrder.IdOrder,
                Harga = item.Harga,
                JmlBarang = item.JmlBarang,
                SubTotal = item.Subtotal,
                IdProduk = item.IdProduk
            });
        }

        await _orderService.Checkout(newOrder);

        await _keranjangService.Clear(idPembeli);

        return RedirectToAction(nameof(CheckoutBerhasil));
    }


    [HttpPost]
    public async Task<IActionResult> Konfirmasi(int? IdOrder)
    {
        if (IdOrder == null)
        {
            return BadRequest();
        }

        //SOLID Principle
        if (!await _orderService.SudahDibayar(IdOrder.Value))
        {
            return BadRequest();
        }

        await _orderService.UpdateStatus(IdOrder.Value, AppConstant.StatusOrder.DIPROSES);

        return RedirectToAction(nameof(DetailOrder), new
        {
            id = IdOrder.Value
        });
    }

    public async Task<IActionResult> Bayar(BayarRequestViewModel request)
    {

        if (!ModelState.IsValid)
        {
            return RedirectToAction(nameof(OrderUserDetail), new
            {
                id = request.IdOrder
            });
        }

        //Simpan file
        string fileName = string.Empty;
        int idPembeli = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt();

        if (request.FileBuktiBayar != null)
        {
            fileName = $"{Guid.NewGuid()}-{request.FileBuktiBayar?.FileName}";

            string filePathName = _iWebHost.WebRootPath + "\\images\\payments\\" + fileName;

            using (var streamWriter = System.IO.File.Create(filePathName))
            {
                //await streamWriter.WriteAsync(Common.StreamToBytes(request.GambarFile.OpenReadStream()));
                //using extension to convert stream to bytes
                await streamWriter.WriteAsync(request.FileBuktiBayar.OpenReadStream().ToBytes());
            }
        }

        Pembayaran newBayar = new Pembayaran()
        {
            FileBuktiBayar = "images/payments/" + fileName,
            IdPembeli = idPembeli,
            IdOrder = request.IdOrder,
            TglBayar = request.TglBayar,
            Tujuan = request.Tujuan,
            JmlBayar = request.JmlBayar,
            MetodePembayaran = request.MetodePembayaran,
            Note = request.Note,
            Pajak = 11000,
            Status = string.Empty
        };

        await _orderService.Bayar(newBayar);

        await _orderService.UpdateStatus(request.IdOrder, AppConstant.StatusOrder.DIBAYAR);

        return RedirectToAction(nameof(OrderUserDetail), new
        {
            id = request.IdOrder
        });
    }

    public async Task<IActionResult> Review(UlasanRequestViewModel request)
    {
        int idPembeli = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt();

        string fileName = string.Empty;
        
        if (request.Gambar != null)
        {
            fileName = $"{Guid.NewGuid()}-{request.Gambar?.FileName}";

            string filePathName = _iWebHost.WebRootPath + "\\images\\ulasan\\" + fileName;

            using (var streamWriter = System.IO.File.Create(filePathName))
            {
                //await streamWriter.WriteAsync(Common.StreamToBytes(request.GambarFile.OpenReadStream()));
                //using extension to convert stream to bytes
                await streamWriter.WriteAsync(request.Gambar.OpenReadStream().ToBytes());
            }
        }

            Ulasan ulasan = new Ulasan{
            IdOrder = request.IdOrder,
            IdPembeli = idPembeli,
            Komentar = request.Komentar,
            Rating = request.Rating,
            Gambar = string.IsNullOrEmpty(fileName)? string.Empty : "images/ulasan/" + fileName,
        };


        await _orderService.Ulas(ulasan);

        await _orderService.UpdateStatus(request.IdOrder, AppConstant.StatusOrder.DITERIMA);

        return RedirectToAction(nameof(OrderUserDetail), new {
            id = request.IdOrder
        });
    }

    public IActionResult CheckoutBerhasil()
    {
        return View();
    }
}