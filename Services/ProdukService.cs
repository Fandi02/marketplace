using marketplace.Interfaces;
using marketplace.Datas;
using marketplace.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace marketplace.Services;
public class ProdukService : BaseDbService, IProdukService
{
    public ProdukService(marketplaceContext dbContext) : base(dbContext)
    {
    }

    public async Task<Produk> Add(Produk obj, int idKategori)
    {
        if(await dbContext.Produks.AnyAsync(x=>x.IdProduk == obj.IdProduk)){
            throw new InvalidOperationException($"Produk with ID {obj.IdProduk} is already exist");
        }

        await dbContext.AddAsync(obj);
        await dbContext.SaveChangesAsync();

        dbContext.ProdukKategoris.Add(new ProdukKategori{
            IdKategori = idKategori,
            IdProduk = obj.IdProduk,
        });
        
        return obj;
    }

    public async Task<Produk> Add(Produk obj)
    {
        if(await dbContext.Produks.AnyAsync(x=>x.IdProduk == obj.IdProduk)){
            throw new InvalidOperationException($"Produk with ID {obj.IdProduk} is already exist");
        }

        await dbContext.AddAsync(obj);
        await dbContext.SaveChangesAsync();
        
        return obj;
    }

    public async Task<bool> Delete(int id)
    {
        var Produk = await dbContext.Produks.FirstOrDefaultAsync(x=>x.IdProduk == id);

        if(Produk == null) {
            throw new InvalidOperationException($"Produk with ID {id} doesn't exist");
        }

        dbContext.ProdukKategoris.RemoveRange(dbContext.ProdukKategoris.Where(x=>x.IdProduk == id));
        dbContext.Remove(Produk);
        await dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<List<Produk>> Get(int limit, int offset, string keyword)
    {
        if(string.IsNullOrEmpty(keyword)){
            keyword = "";
        }

        return await dbContext.Produks
        .Skip(offset)
        .Take(limit).ToListAsync();
    }

    public async Task<Produk?> Get(int id)
    {
        var result = await dbContext.Produks.FirstOrDefaultAsync();

        if(result == null)
        {
            throw new InvalidOperationException($"Produk with ID {id} doesn't exist");
        }

        return result;
    }

    public Task<Produk?> Get(Expression<Func<Produk, bool>> func)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Produk>> GetAll()
    {
        return await dbContext.Produks
        .Include(x=>x.ProdukKategoris)
        .ThenInclude(x=>x.IdKategoriNavigation)
        .ToListAsync();
    }

    public async Task<Produk> Update(Produk obj)
    {
        if(obj == null)
        {
            throw new ArgumentNullException("Produk cannot be null");
        }

        var Produk = await dbContext.Produks.FirstOrDefaultAsync(x=>x.IdProduk == obj.IdProduk);

        if(Produk == null) {
            throw new InvalidOperationException($"Produk with ID {obj.IdProduk} doesn't exist in database");
        }

        Produk.Nama = obj.Nama;
        Produk.Deskripsi = obj.Deskripsi;
        
        dbContext.Update(Produk);
        await dbContext.SaveChangesAsync();

        return Produk;
    }
}