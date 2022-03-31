namespace marketplace.Interfaces;
using marketplace.Datas.Entities;
public interface IAccountService
{
    Task<Admin> Login(string username, string password);
}