using marketplace.Interfaces;
using marketplace.Datas;
using marketplace.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace marketplace.Services;
public class BaseDbService
{
    protected readonly marketplaceContext dbContext;

    public BaseDbService(marketplaceContext dbContext)
    {
        this.dbContext = dbContext;
    }
}