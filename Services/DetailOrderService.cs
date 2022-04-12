using marketplace.Interfaces;
using marketplace.Datas;
using marketplace.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using marketplace.ViewModels;
using marketplace.Controllers;

namespace marketplace.Services;
public class DetailOrderService : BaseDbService, IDetailOrderService
{
    public DetailOrderService(marketplaceContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<StatusOrder>> Get()
    {
        return await dbContext.StatusOrders.ToListAsync();
    }

    public Task<List<DetailOrder>> GetAll(int idPembeli)
    {
        throw new NotImplementedException();
    }

    public Task GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<List<DetailOrder>> GetDetailOrder(int IdPembeli)
    {
        throw new NotImplementedException();
    }
}