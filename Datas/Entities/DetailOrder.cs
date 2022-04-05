using System;
using System.Collections.Generic;

namespace marketplace.Datas.Entities
{
    public partial class DetailOrder
    {
        public int Id { get; set; }
        public int IdOrder { get; set; }
        public int IdProduk { get; set; }
        public decimal Harga { get; set; }
        public int JmlBarang { get; set; }
        public decimal SubTotal { get; set; }

        public virtual Order IdOrderNavigation { get; set; } = null!;
        public virtual Produk IdProdukNavigation { get; set; } = null!;
    }
}
