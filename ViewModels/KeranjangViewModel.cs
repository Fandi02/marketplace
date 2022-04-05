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
    public string Gambar { get; set; }
    public string NamaProduk { get; set; }
    public int IdPembeli { get; set; }
    public int JmlBarang { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Harga { get; set; }

} 