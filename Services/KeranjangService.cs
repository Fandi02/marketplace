using marketplace.Interfaces;
using marketplace.Datas;
using marketplace.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using marketplace.ViewModels;

namespace marketplace.Services;
public class KeranjangService : BaseDbService, IKeranjangService
{
    private readonly IProdukService _produkService;
    public KeranjangService(marketplaceContext dbContext, IProdukService produkService) : base(dbContext)
    {
        _produkService = produkService;
    }

    public async Task<Keranjang> Add(Keranjang obj)
    {
        if (await dbContext.Keranjangs.AnyAsync(x => x.IdProduk == obj.IdProduk && x.IdPembeli == obj.IdPembeli))
        {
            return obj;
        }

        //get data produk
        var produk = await _produkService.Get(obj.IdProduk);

        if (produk == null)
        {
            throw new InvalidOperationException("Produk tidak ditemukan");
        }

        if (obj.JmlBarang < 1)
        {
            obj.JmlBarang = 1;
        }

        //rumus subtotal = harga * jumlah produk
        obj.SubTotal = produk.Harga * obj.JmlBarang;

        await dbContext.AddAsync(obj);
        await dbContext.SaveChangesAsync();

        return obj;
    }

    public Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Keranjang>> Get(int limit, int offset, string keyword)
    {
        throw new NotImplementedException();
    }

    public Task<Keranjang?> Get(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Keranjang?> Get(Expression<Func<Keranjang, bool>> func)
    {
        throw new NotImplementedException();
    }

    public Task<List<Keranjang>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Keranjang> Update(Keranjang obj)
    {
        throw new NotImplementedException();
    }

    async Task<List<KeranjangViewModel>> IKeranjangService.Get(int idPembeli)
    {
        var result = await (from a in dbContext.Keranjangs
                            join b in dbContext.Produks on a.IdProduk equals b.IdProduk
                            where a.IdPembeli == idPembeli
                            select new KeranjangViewModel
                            {
                                Id = a.IdKeranjang,
                                IdCustomer = a.IdPembeli,
                                IdProduk = a.IdProduk,
                                Image = b.Gambar,
                                JmlBarang = a.JmlBarang,
                                Subtotal = a.SubTotal,
                                NamaProduk = b.Nama
                            }).ToListAsync();

        return result;
    }
}