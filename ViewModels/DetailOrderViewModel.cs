using System.ComponentModel.DataAnnotations;
using marketplace.Datas.Entities;

namespace marketplace.ViewModels;
public class DetailOrderViewModel
{
    public DetailOrderViewModel()
    {
    }

    public int Id { get; set; }
    public int IdOrder { get; set; }
    public string IdProduk { get; set; }
    public string Harga { get; set; }
    public int SubTotal { get; set; }
    public int JmlBarang { get; set; }
    public decimal Subtotal { get; set; }
    

} 