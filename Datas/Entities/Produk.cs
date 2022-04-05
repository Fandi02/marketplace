using System;
using System.Collections.Generic;

namespace marketplace.Datas.Entities
{
    public partial class Produk
    {
        public Produk()
        {
            DetailOrders = new HashSet<DetailOrder>();
            Keranjangs = new HashSet<Keranjang>();
            ProdukKategoris = new HashSet<ProdukKategori>();
        }

        public int IdProduk { get; set; }
        public string Nama { get; set; } = null!;
        public string Deskripsi { get; set; } = null!;
        public decimal Harga { get; set; }
        public int Stok { get; set; }
        public string Gambar { get; set; } = null!;

        public virtual ICollection<DetailOrder> DetailOrders { get; set; }
        public virtual ICollection<Keranjang> Keranjangs { get; set; }
        public virtual ICollection<ProdukKategori> ProdukKategoris { get; set; }
    }
}
