using System.ComponentModel.DataAnnotations;
using marketplace.Datas.Entities;

namespace marketplace.ViewModels
{
    public class OrderViewModel
    {
        public OrderViewModel()
        {
            Details = new List<OrderDetailViewModel>();
        }
        public int IdOrder { get; set; }
        public string Nama { get; set; }
        public string NoHp { get; set; }
        public string Email { get; set; }
        public DateTime TglOrder { get; set; }
        public int TotalQty
        {
            get
            {
                return (Details == null || !Details.Any()) ? 0 : Details.Sum(x => x.Qty);
            }
        }
        public decimal TotalBayar { get; set; }
        public string Status { get; set; }
        public int IdAlamat { get; set; }
        public string Alamat { get; set; }

        public List<OrderDetailViewModel> Details { get; set; }
        public PembayaranViewModel Pembayaran { get; set; }
        public PengirimanViewModel Pengiriman { get; set; }
        public int IdStatus { get; set; }
    }
}