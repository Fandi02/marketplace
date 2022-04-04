using System.ComponentModel.DataAnnotations;
using marketplace.Datas.Entities;

namespace marketplace.ViewModels;
public class KeranjangViewModel
{
    public KeranjangViewModel()
    {
    }

    public int Id { get; set; }
    public int IdProduk { get; set; }
    public string Image { get; set; }
    public string NamaProduk { get; set; }
    public int IdCustomer { get; set; }
    public int JmlBarang { get; set; }
    public decimal Subtotal { get; set; }

} 