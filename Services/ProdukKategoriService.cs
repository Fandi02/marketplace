using marketplace.Interfaces;
using marketplace.Datas;
using marketplace.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace marketplace.Services;
public class ProdukKategoriService : BaseDbService, IProdukKategoriService
{
    public ProdukKategoriService(marketplaceContext dbContext) : base(dbContext)
    {
    }

    public async Task<int[]> GetKategoriIds(int produkId)
    {
        var result = await dbContext.ProdukKategoris
        .Where(x => x.IdProduk == produkId)
        .Select(x => x.IdKategori)
        .Distinct()
        .ToArrayAsync();

        return result;
    }
    public async Task Remove(int produkId, int idKategori)
    {
        var item = await dbContext.ProdukKategoris.FirstOrDefaultAsync(x => x.IdProduk == produkId && x.IdKategori == idKategori);

        if (item == null)
        {
            return;
        }

        dbContext.ProdukKategoris.Remove(item);

        await dbContext.SaveChangesAsync();
    }
}