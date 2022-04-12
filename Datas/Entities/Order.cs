using System;
using System.Collections.Generic;

namespace marketplace.Datas.Entities
{
    public partial class Order
    {
        public Order()
        {
            DetailOrders = new HashSet<DetailOrder>();
            Pembayarans = new HashSet<Pembayaran>();
            Pengirimen = new HashSet<Pengiriman>();
            Ulasans = new HashSet<Ulasan>();
        }

        public int IdOrder { get; set; }
        public DateTime TglTransaksi { get; set; }
        public decimal JmlBayar { get; set; }
        public int IdPembeli { get; set; }
        public int IdStatusOrder { get; set; }
        public int IdAlamat { get; set; }
        public string Notes { get; set; } = null!;

        public virtual Alamat IdAlamatNavigation { get; set; } = null!;
        public virtual Pembeli IdPembeliNavigation { get; set; } = null!;
        public virtual StatusOrder IdStatusOrderNavigation { get; set; } = null!;
        public virtual ICollection<DetailOrder> DetailOrders { get; set; }
        public virtual ICollection<Pembayaran> Pembayarans { get; set; }
        public virtual ICollection<Pengiriman> Pengirimen { get; set; }
        public virtual ICollection<Ulasan> Ulasans { get; set; }
    }
}
