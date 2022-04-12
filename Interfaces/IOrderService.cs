namespace marketplace.Interfaces;
using marketplace.Datas.Entities;
using marketplace.ViewModels;
public interface IOrderService
{
    Task<Order> Checkout(Order newOrder);
    Task<List<OrderViewModel>> Get(int idPembeli);
    Task<List<OrderViewModel>> GetV2(int limit, int offset, int? status = null, DateTime? dateTime = null);    
    Task UpdateStatus(int idOrder, short dIBAYAR);
    Task Bayar(Pembayaran newBayar);
    Task<OrderViewModel> GetDetail(int IdOrder, int IdPembeli);
    Task<bool> SudahDibayar(int idOrder);
    Task Kirim(Pengiriman dataPengiriman);
    Task<OrderViewModel> GetDetailAdmin(int idOrder);
    Task Ulas(Ulasan ulasan);
}