namespace marketplace.Interfaces;
using marketplace.Datas.Entities;
using marketplace.ViewModels;
public interface IOrderService
{
    Task<Order> Checkout(Order newOrder);
    Task<List<OrderViewModel>> Get(int idPembeli);    
}