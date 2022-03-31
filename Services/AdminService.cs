using marketplace.Interfaces;
using marketplace.Datas;
using marketplace.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace marketplace.Services;

public class AdminService : BaseDbService, IAdminService
{
    public AdminService(marketplaceContext dbContext) : base(dbContext)
    {
    }

    public async Task<Admin> Add(Admin obj)
    {
        if(await dbContext.Admins.AnyAsync(x=>x.IdAdmin == obj.IdAdmin)){
            throw new InvalidOperationException($"Admin with ID {obj.IdAdmin} is already exist");
        }

        await dbContext.AddAsync(obj);
        await dbContext.SaveChangesAsync();

        return obj;
    }

    public async Task<bool> Delete(int id)
    {
        var admin = await dbContext.Admins.FirstOrDefaultAsync(x=>x.IdAdmin == id);

        if(admin == null) {
            throw new InvalidOperationException($"Admin with ID {id} doesn't exist");
        }
        dbContext.Admins.RemoveRange(dbContext.Admins.Where(x=>x.IdAdmin == id));
        dbContext.Remove(admin);
        await dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<List<Admin>> Get(int limit, int offset, string keyword)
    {
        if(string.IsNullOrEmpty(keyword)){
            keyword = "";
        }

        return await dbContext.Admins
        .Skip(offset)
        .Take(limit).ToListAsync();
    }

    public async Task<Admin?> Get(int id)
    {
        var result = await dbContext.Admins.FirstOrDefaultAsync();

        if(result == null)
        {
            throw new InvalidOperationException($"Admin with ID {id} doesn't exist");
        }

        return result;
    }

    public Task<Admin?> Get(Expression<Func<Admin, bool>> func)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Admin>> GetAll()
    {
        return await dbContext.Admins.ToListAsync();
    }

    public async Task<Admin> Update(Admin obj)
    {
        if(obj == null)
        {
            throw new ArgumentNullException("Admin cannot be null");
        }

        var admin = await dbContext.Admins.FirstOrDefaultAsync(x=>x.IdAdmin == obj.IdAdmin);

        if(admin == null) {
            throw new InvalidOperationException($"Admin with ID {obj.IdAdmin} doesn't exist in database");
        }

        admin.Nama = obj.Nama;
        admin.NoHp = obj.NoHp;
        admin.Username = obj.Username;
        admin.Password = obj.Password;
        admin.Email = obj.Email;
        
        dbContext.Update(admin);
        await dbContext.SaveChangesAsync();

        return admin;
    }
}