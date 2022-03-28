using System;
using System.Collections.Generic;

namespace marketplace.Datas.Entities
{
    public partial class Pembeli
    {
        public Pembeli()
        {
            Keranjangs = new HashSet<Keranjang>();
            Orders = new HashSet<Order>();
            Pembayarans = new HashSet<Pembayaran>();
        }

        public int IdPembeli { get; set; }
        public string Nama { get; set; } = null!;
        public string NoHp { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Foto { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? IsAdmin { get; set; }

        public virtual ICollection<Keranjang> Keranjangs { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Pembayaran> Pembayarans { get; set; }
    }
}
