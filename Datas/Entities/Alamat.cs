using System;
using System.Collections.Generic;

namespace marketplace.Datas.Entities
{
    public partial class Alamat
    {
        public Alamat()
        {
            Orders = new HashSet<Order>();
            Pengirimen = new HashSet<Pengiriman>();
        }

        public int IdAlamat { get; set; }
        public int IdPembeli { get; set; }
        public string Provinsi { get; set; } = null!;
        public string Kota { get; set; } = null!;
        public string Kecamatan { get; set; } = null!;
        public string Desa { get; set; } = null!;
        public string KodePos { get; set; } = null!;
        public string Deskripsi { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Pengiriman> Pengirimen { get; set; }
    }
}
