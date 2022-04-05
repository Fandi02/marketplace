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
            Id = a.IdAlamat,
            Status = b.Nama,
            // TglOrder = a.TglTransaksi,
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
                }).ToList()
        }).ToListAsync();

        return result;
    }
}