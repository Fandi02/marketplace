using System.ComponentModel.DataAnnotations;
using marketplace.Datas.Entities;

namespace marketplace.ViewModels;
    public class KeranjangUpdateViewModel
    {
         public KeranjangUpdateViewModel()
    {
    }

    [Required]
    public int Id { get; set; }
    [Required]
    public int JmlBarang { get; set; }
    }
