namespace marketplace.Interfaces;
using marketplace.Datas.Entities;
public interface IProdukService : ICRUDService<Produk>
{
    Task<Produk> Add(Produk obj, int idKategori);
}