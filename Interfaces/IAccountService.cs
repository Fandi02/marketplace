namespace marketplace.Interfaces;
using marketplace.Datas.Entities;
using marketplace.ViewModels;
public interface IAccountService
{
    Task<Admin> Login(string username, string password);
    Task<Pembeli> LoginPembeli(string username, string password);
    Task<Pembeli> Register(RegisterViewModel request);
}