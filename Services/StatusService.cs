using marketplace.Interfaces;
using marketplace.Datas;
using marketplace.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using marketplace.ViewModels;

namespace marketplace.Services;
public class StatusService : BaseDbService, IStatusService
{
    public StatusService(marketplaceContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<StatusOrder>> Get()
    {
        return await dbContext.StatusOrders.ToListAsync();
    }
}