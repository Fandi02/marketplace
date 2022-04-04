using marketplace.Interfaces;
using marketplace.Datas;
using marketplace.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace marketplace.Services;
public class KategoriService : BaseDbService, IKategoriService
{
    public KategoriService(marketplaceContext dbContext) : base(dbContext)
    {
    }

    public async Task<Kategori> Add(Kategori obj)
    {
        if(await dbContext.Kategoris.AnyAsync(x=>x.IdKategori == obj.IdKategori)){
            throw new InvalidOperationException($"Kategori with ID {obj.IdKategori} is already exist");
        }

        await dbContext.AddAsync(obj);
        await dbContext.SaveChangesAsync();

        return obj;
    }

    public async Task<bool> Delete(int id)
    {
        var kategori = await dbContext.Kategoris.FirstOrDefaultAsync(x=>x.IdKategori == id);

        if(kategori == null) {
            throw new InvalidOperationException($"Kategori with ID {id} doesn't exist");
        }
        dbContext.ProdukKategoris.RemoveRange(dbContext.ProdukKategoris.Where(x=>x.IdKategori == id));
        dbContext.Remove(kategori);
        await dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<List<Kategori>> Get(int limit, int offset, string keyword)
    {
        if(string.IsNullOrEmpty(keyword)){
            keyword = "";
        }

        return await dbContext.Kategoris
        .Skip(offset)
        .Take(limit).ToListAsync();
    }

    public async Task<Kategori?> Get(int id)
    {
        var result = await dbContext.Kategoris.FirstOrDefaultAsync(x=>x.IdKategori == id);

        if(result == null)
        {
            throw new InvalidOperationException($"Kategori with ID {id} doesn't exist");
        }

        return result;
    }

    public Task<Kategori?> Get(Expression<Func<Kategori, bool>> func)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Kategori>> GetAll()
    {
        return await dbContext.Kategoris.ToListAsync();
    }

    public async Task<Kategori> Update(Kategori obj)
    {
        if(obj == null)
        {
            throw new ArgumentNullException("Kategori cannot be null");
        }

        var kategori = await dbContext.Kategoris.FirstOrDefaultAsync(x=>x.IdKategori == obj.IdKategori);

        if(kategori == null) {
            throw new InvalidOperationException($"Kategori with ID {obj.IdKategori} doesn't exist in database");
        }

        kategori.Nama = obj.Nama;
        kategori.Deskripsi = obj.Deskripsi;
        kategori.Icon = obj.Icon;
        
        dbContext.Update(kategori);
        await dbContext.SaveChangesAsync();

        return kategori;
    }
}