using System;
using System.Collections.Generic;

namespace marketplace.Datas.Entities
{
    public partial class Pengiriman
    {
        public int IdPengiriman { get; set; }
        public int IdOrder { get; set; }
        public string NoResi { get; set; } = null!;
        public string Kurir { get; set; } = null!;
        public int Ongkir { get; set; }
        public int IdAlamat { get; set; }
        public string Keterangan { get; set; } = null!;
        public string Status { get; set; } = null!;

        public virtual Alamat IdAlamatNavigation { get; set; } = null!;
        public virtual Order IdOrderNavigation { get; set; } = null!;
    }
}
