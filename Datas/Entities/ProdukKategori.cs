using System;
using System.Collections.Generic;

namespace marketplace.Datas.Entities
{
    public partial class ProdukKategori
    {
        public int IdProdukKategori { get; set; }
        public int IdProduk { get; set; }
        public int IdKategori { get; set; }

        public virtual Kategori IdKategoriNavigation { get; set; } = null!;
        public virtual Produk IdProdukNavigation { get; set; } = null!;
    }
}
