namespace marketplace.Interfaces;
using marketplace.Datas.Entities;
public interface IProdukKategoriService
{
    Task<int[]> GetKategoriIds(int produkId);
    Task Remove(int produkId, int idKategori);
} 