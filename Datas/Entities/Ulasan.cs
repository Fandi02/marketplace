using System;
using System.Collections.Generic;

namespace marketplace.Datas.Entities
{
    public partial class Ulasan
    {
        public int IdUlasan { get; set; }
        public int IdOrder { get; set; }
        public int IdPembeli { get; set; }
        public string Komentar { get; set; } = null!;
        public string Gambar { get; set; } = null!;
        public int Rating { get; set; }

        public virtual Order IdOrderNavigation { get; set; } = null!;
        public virtual Pembeli IdPembeliNavigation { get; set; } = null!;
    }
}
