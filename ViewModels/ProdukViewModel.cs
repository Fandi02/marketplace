using System.ComponentModel.DataAnnotations;
using marketplace.Datas.Entities;
namespace marketplace.ViewModels
{
    public class ProdukViewModel
    {
        public ProdukViewModel()
        {
            Kategories = new List<KategoriViewModel>();
        }
        public ProdukViewModel(int idProduk, string nama, string deskripsi, decimal harga)
        {
            IdProduk = idProduk;
            Nama = nama;
            Deskripsi = deskripsi;
            Harga = harga;
            Stok = 100;
            KategoriId = Array.Empty<int>();
            Kategories = new List<KategoriViewModel>();
        }
        public ProdukViewModel(Produk item){
            IdProduk = item.IdProduk;
            Nama = item.Nama;
            Deskripsi = item.Deskripsi;
            Harga = item.Harga;
            Stok = item.Stok;
            Gambar = item.Gambar;
            // KategoriId = item.ProdukKategoris.Select(x => x.IdKategori).ToArray();
            // Kategories = item.ProdukKategoris.Select(x => new KategoriViewModel(x.IdKategoriNavigation)).ToList();
        }
        public int IdProduk { get; set; }
        [Required]
        public string Nama { get; set; }
        public string Deskripsi { get; set; }
        [Required]
        public decimal Harga { get; set; }
        public int Stok { get; set; }
        public string? Gambar { get; set; }
        public string GambarSrc { 
            get {
                return (string.IsNullOrEmpty(Gambar) ? "~/images/default.png" : Gambar);
        }}
        public IFormFile? GambarFile { get; set; }
        public int[] KategoriId{ get; set; }
        public List<KategoriViewModel> Kategories { get; set; }
        
        public Produk ConvertToDbModel(){
            return new Produk {
                IdProduk = this.IdProduk,
                Nama = this.Nama,
                Deskripsi = this.Deskripsi,
                Harga = this.Harga,
                Stok = this.Stok,
                Gambar = this.Gambar?? string.Empty,
            };
        }
    }
}