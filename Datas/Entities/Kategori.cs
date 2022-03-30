using System;
using System.Collections.Generic;

namespace marketplace.Datas.Entities
{
    public partial class Kategori
    {
        public Kategori()
        {
            ProdukKategoris = new HashSet<ProdukKategori>();
        }

        public int IdKategori { get; set; }
        public string Nama { get; set; } = null!;
        public string Deskripsi { get; set; } = null!;
        public string Icon { get; set; } = null!;

        public virtual ICollection<ProdukKategori> ProdukKategoris { get; set; }
    }
}
