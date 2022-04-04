using marketplace.Interfaces;
using marketplace.Datas;
using marketplace.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using marketplace.ViewModels;

namespace marketplace.Services;
public class AccountService : BaseDbService, IAccountService
{
    public AccountService(marketplaceContext dbContext) : base(dbContext)
    {
    }

    public async Task<Admin> Login(string username, string password)
    {
        var result = await dbContext.Admins.FirstOrDefaultAsync(x=>x.Username == username && x.Password == password);

        return result;
    }

     public async Task<Pembeli> LoginPembeli(string username, string password)
    {
        return await dbContext.Pembelis.FirstOrDefaultAsync(x=>x.Username == username && x.Password == password);
    }

    public async Task<Pembeli> Register(RegisterViewModel request){
        //check username sudah ada atau belum di db
        if(await dbContext.Pembelis.AnyAsync(x=>x.Username == request.Username)){
            throw new InvalidOperationException($"{request.Username} already exist");
        }

        //check email exist or not
        if(await dbContext.Pembelis.AnyAsync(x=>x.Email == request.Email)){
            throw new InvalidOperationException($"{request.Email} already exist");
        }

        //check nohp exist or not
        if(await dbContext.Pembelis.AnyAsync(x=>x.NoHp == request.NoHp)){
            throw new InvalidOperationException($"{request.NoHp} already exist");
        }

        var newCustomer = request.ConvertToDataModel();
        await dbContext.Pembelis.AddAsync(newCustomer);

        await dbContext.SaveChangesAsync();

        return newCustomer; 
    }
} 