using marketplace.Interfaces;
using marketplace.Datas;
using marketplace.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using marketplace.ViewModels;

namespace marketplace.Services;
public class OrderService : BaseDbService, IOrderService
{
    public OrderService(marketplaceContext dbContext) : base(dbContext)
    {
    }



    public async Task<Order> Checkout(Order newOrder)
    {
        await dbContext.AddAsync(newOrder);
        await dbContext.SaveChangesAsync();

        return newOrder;
    }

    public async Task<List<OrderViewModel>> Get(int idPembeli)
    {
        var result = await (from a in dbContext.Orders

                            join b in dbContext.StatusOrders on a.IdStatusOrder equals b.IdStatusOrder
                            join alamat in dbContext.Alamats on a.IdAlamat equals alamat.IdAlamat
                            select new OrderViewModel
                            {
                                IdOrder = a.IdOrder,
                                Status = b.Nama,
                                TglOrder = a.TglTransaksi,
                                TotalBayar = a.JmlBayar,
                                Details = (from c in dbContext.DetailOrders
                                           join d in dbContext.Produks on c.IdProduk equals d.IdProduk
                                           where c.IdOrder == a.IdOrder
                                           select new OrderDetailViewModel
                                           {
                                               Id = c.Id,
                                               Produk = d.Nama,
                                               Harga = c.Harga,
                                               Qty = c.JmlBarang,
                                               SubTotal = c.SubTotal
                                           }).ToList(),
                            }).ToListAsync();

        return result;
    }

    public Task<List<OrderViewModel>> Get(int idPembeli, int IdOrder)
    {
        throw new NotImplementedException();
    }

    public async Task<OrderViewModel> GetDetail(int IdOrder, int IdPembeli)
    {
        var result = await (from a in dbContext.Orders
                                //inner join
                            join b in dbContext.StatusOrders on a.IdStatusOrder equals b.IdStatusOrder
                            join alamat in dbContext.Alamats on a.IdAlamat equals alamat.IdAlamat

                            //end join
                            //left join
                            join pembayaran in dbContext.Pembayarans on a.IdOrder equals pembayaran.IdOrder into tempPembayaran
                            from pembayaran in tempPembayaran.DefaultIfEmpty()
                                //end join

                            join pengiriman in dbContext.Pengirimen on a.IdOrder equals pengiriman.IdOrder into tempPengiriman
                            from pengiriman in tempPengiriman.DefaultIfEmpty()

                            where a.IdOrder == IdOrder && a.IdPembeli == IdPembeli
                            select new OrderViewModel
                            {
                                IdOrder = a.IdOrder,
                                Status = b.Nama,
                                IdStatus = b.IdStatusOrder,
                                TglOrder = a.TglTransaksi,
                                TotalBayar = a.JmlBayar,
                                Alamat = alamat.Deskripsi,
                                IdAlamat = alamat.IdAlamat,
                                //mendapatkan data detail item order
                                Details = (from c in dbContext.DetailOrders
                                           join d in dbContext.Produks on c.IdProduk equals d.IdProduk
                                           where c.IdOrder == a.IdOrder
                                           select new OrderDetailViewModel
                                           {
                                               Id = c.Id,
                                               Produk = d.Nama,
                                               Harga = c.Harga,
                                               Qty = c.JmlBarang,
                                               SubTotal = c.SubTotal
                                           }).ToList(),

                                //mendapatkan data pembayaran jika sudah ada 
                                Pembayaran = pembayaran == null ? new PembayaranViewModel() : new PembayaranViewModel
                                {
                                    IdPembayaran = pembayaran.IdPembayaran,
                                    Metode = pembayaran.MetodePembayaran,
                                    Tujuan = pembayaran.Tujuan,
                                    JmlBayar = pembayaran.JmlBayar,
                                    Note = pembayaran.Note,
                                    Pajak = pembayaran.Pajak,
                                    Status = pembayaran.Status,
                                    TglBayar = pembayaran.TglBayar,
                                    FileBuktiBayar = pembayaran.FileBuktiBayar
                                },

                                Pengiriman = pengiriman == null ? new PengirimanViewModel() : new PengirimanViewModel
                                {
                                    IdPengiriman = pengiriman.IdPengiriman,
                                    Keterangan = pengiriman.Keterangan,
                                    Kurir = pengiriman.Kurir,
                                    NoResi = pengiriman.NoResi,
                                    Ongkir = pengiriman.Ongkir,
                                    Status = pengiriman.Status,
                                }
                            }).FirstOrDefaultAsync();

        return result;
    }


    public async Task<OrderViewModel> GetDetailAdmin(int idOrder)
    {
        var result = await (from a in dbContext.Orders
                                //inner join
                            join b in dbContext.StatusOrders on a.IdStatusOrder equals b.IdStatusOrder
                            join alamat in dbContext.Alamats on a.IdAlamat equals alamat.IdAlamat
                            join pembeli in dbContext.Pembelis on a.IdPembeli equals pembeli.IdPembeli
                            //end join

                            //left join
                            join pembayaran in dbContext.Pembayarans on a.IdOrder equals pembayaran.IdOrder into tempPembayaran
                            from pembayaran in tempPembayaran.DefaultIfEmpty()
                                //end join
                            where a.IdOrder == idOrder
                            select new OrderViewModel
                            {
                                IdOrder = a.IdOrder,
                                Nama = pembeli.Nama,
                                NoHp = pembeli.NoHp,
                                Email = pembeli.Email,
                                Status = b.Nama,
                                IdStatus = b.IdStatusOrder,
                                TglOrder = a.TglTransaksi,
                                TotalBayar = a.JmlBayar,
                                Alamat = alamat.Deskripsi,
                                IdAlamat = alamat.IdAlamat,
                                //mendapatkan data detail item order
                                Details = (from c in dbContext.DetailOrders
                                           join d in dbContext.Produks on c.IdProduk equals d.IdProduk
                                           where c.IdOrder == a.IdOrder
                                           select new OrderDetailViewModel
                                           {
                                               Id = c.Id,
                                               Produk = d.Nama,
                                               Harga = c.Harga,
                                               Qty = c.JmlBarang,
                                               SubTotal = c.SubTotal
                                           }).ToList(),

                                //mendapatkan data pembayaran jika sudah ada 
                                Pembayaran = pembayaran == null ? new PembayaranViewModel() : new PembayaranViewModel
                                {
                                    IdPembayaran = pembayaran.IdPembayaran,
                                    Metode = pembayaran.MetodePembayaran,
                                    Tujuan = pembayaran.Tujuan,
                                    JmlBayar = pembayaran.JmlBayar,
                                    Note = pembayaran.Note,
                                    Pajak = pembayaran.Pajak,
                                    Status = pembayaran.Status,
                                    TglBayar = pembayaran.TglBayar,
                                    FileBuktiBayar = pembayaran.FileBuktiBayar
                                }
                            }).FirstOrDefaultAsync();
        return result;
    }

    public async Task Kirim(Pengiriman dataPengiriman)
    {
        if(await dbContext.Pengirimen.AnyAsync(x=>x.IdOrder == dataPengiriman.IdOrder))
        {
            throw new InvalidOperationException("Pengiriman sudah dilakukan");
        }

        await dbContext.AddAsync(dataPengiriman);
        await dbContext.SaveChangesAsync();
    }

    public Task GetDetail(object value)
    {
        throw new NotImplementedException();
    }

    public async Task<List<OrderViewModel>> GetV2(int limit, int offset, int? status, DateTime? date)
    {
        var selectCondition = (from a in dbContext.Orders
                               join b in dbContext.StatusOrders on a.IdStatusOrder equals b.IdStatusOrder
                               join alamat in dbContext.Alamats on a.IdAlamat equals alamat.IdAlamat
                               select new OrderViewModel
                               {
                                   IdOrder = a.IdOrder,
                                   Status = b.Nama,
                                   TglOrder = a.TglTransaksi,
                                   TotalBayar = a.JmlBayar
                               }).AsQueryable();

        if (status != null)
        {
            throw new NotImplementedException();
            selectCondition = selectCondition.Where(x => x.IdStatus == status.Value);
        }

        if (date != null)
        {
            selectCondition = selectCondition.Where(x => x.TglOrder.Date == date.Value.Date);
        }

        return await selectCondition
        .Skip(offset)
        .Take(limit)
        .ToListAsync();
    }


    public async Task<bool> SudahDibayar(int idOrder)
    {
        return await dbContext.Orders.AnyAsync(x=>x.IdOrder == idOrder && x.IdStatusOrder == AppConstant.StatusOrder.DIBAYAR);
    }

    public async Task UpdateStatus(int idOrder, short dIBAYAR)
    {
        var order = await dbContext.Orders.FirstOrDefaultAsync(x => x.IdOrder == idOrder);

        if (order == null)
        {
            throw new ArgumentNullException("Data order tidak ditemukan");
        }

        order.IdStatusOrder = dIBAYAR;

        dbContext.Update(order);
        await dbContext.SaveChangesAsync();
    }

    public Task<OrderViewModel> GetDetailAdmin(int idOrder, int idPembeli)
    {
        throw new NotImplementedException();
    }

    public async Task Bayar(Pembayaran newBayar)
    {
        if (await dbContext.Pembayarans.AnyAsync(x => x.IdOrder == newBayar.IdOrder))
        {
            throw new InvalidOperationException("Pembayaran sudah dilakukan");
        }

        await dbContext.AddAsync(newBayar);
        await dbContext.SaveChangesAsync();
    }

    public async Task Ulas(Ulasan ulasan)
    {
        if(await dbContext.Ulasans.AnyAsync(x=>x.IdOrder == ulasan.IdOrder))
        {
            throw new InvalidOperationException("Anda sudah memberikan review");
        }

        await dbContext.AddAsync(ulasan);
        await dbContext.SaveChangesAsync();
    }
}