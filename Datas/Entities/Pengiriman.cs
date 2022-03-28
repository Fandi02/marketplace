using System;
using System.Collections.Generic;

namespace marketplace.Datas.Entities
{
    public partial class Pengiriman
    {
        public int IdPengiriman { get; set; }
        public int IdOrder { get; set; }
        public int Kurir { get; set; }
        public int Ongkir { get; set; }
        public int IdAlamat { get; set; }

        public virtual Alamat IdAlamatNavigation { get; set; } = null!;
        public virtual Order IdOrderNavigation { get; set; } = null!;
    }
}
