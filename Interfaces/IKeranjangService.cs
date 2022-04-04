namespace marketplace.Interfaces;
using marketplace.Datas.Entities;
using marketplace.ViewModels;
public interface IKeranjangService : ICRUDService<Keranjang>
{
    Task<List<KeranjangViewModel>> Get(int IdPembeli);
}