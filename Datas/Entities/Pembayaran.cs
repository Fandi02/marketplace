using System;
using System.Collections.Generic;

namespace marketplace.Datas.Entities
{
    public partial class Pembayaran
    {
        public int IdPembayaran { get; set; }
        public string MetodePembayaran { get; set; } = null!;
        public int IdOrder { get; set; }
        public int IdPembeli { get; set; }
        public decimal JmlBayar { get; set; }
        public DateTime TglBayar { get; set; }
        public decimal Pajak { get; set; }
        public string Tujuan { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string Note { get; set; } = null!;
        public string FileBuktiBayar { get; set; } = null!;

        public virtual Order IdOrderNavigation { get; set; } = null!;
        public virtual Pembeli IdPembeliNavigation { get; set; } = null!;
    }
}
