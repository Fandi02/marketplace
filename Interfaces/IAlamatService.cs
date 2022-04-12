namespace marketplace.Interfaces;
using marketplace.Datas.Entities;
using marketplace.ViewModels;
public interface IAlamatService : ICRUDService<Alamat>
{
    Task<List<Alamat>> GetAll(int idPembeli);
    Task<List<AlamatViewModel>> GetAlamat(int IdPembeli);
}