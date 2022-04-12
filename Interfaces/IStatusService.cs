namespace marketplace.Interfaces;
using marketplace.Datas.Entities;
using marketplace.ViewModels;

public interface IStatusService
{
    Task<List<StatusOrder>> Get();
}