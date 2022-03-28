using System;
using System.Collections.Generic;

namespace marketplace.Datas.Entities
{
    public partial class Keranjang
    {
        public Keranjang()
        {
            Orders = new HashSet<Order>();
        }

        public int IdKeranjang { get; set; }
        public int IdProduk { get; set; }
        public int IdPembeli { get; set; }
        public int JmlBarang { get; set; }
        public decimal SubTotal { get; set; }

        public virtual Pembeli IdPembeliNavigation { get; set; } = null!;
        public virtual Produk IdProdukNavigation { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
