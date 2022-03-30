using System.ComponentModel.DataAnnotations;
using marketplace.Datas.Entities;

namespace marketplace.ViewModels
{
    public class KategoriViewModel
    {
        public KategoriViewModel()
        {
            Nama = string.Empty;
            Deskripsi = string.Empty;
            Icon = string.Empty;
        }
        public int Id { get; set; }
        [Required]
        public string Nama { get; set; }
        public string Deskripsi { get; set; }
        public string Icon { get; set; }

        public Kategori ConvertToDbModel(){
            return new Kategori {
                IdKategori = this.Id,
                Nama = this.Nama,
                Deskripsi = this.Deskripsi,
                Icon = this.Icon,
            };
        }

        public KategoriViewModel(Kategori item){
            Id =item.IdKategori;
            Nama = item.Nama;
            Deskripsi = item.Deskripsi;
            Icon = item.Icon;
        }
    }
}