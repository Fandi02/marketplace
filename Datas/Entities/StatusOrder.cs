using System;
using System.Collections.Generic;

namespace marketplace.Datas.Entities
{
    public partial class StatusOrder
    {
        public StatusOrder()
        {
            Orders = new HashSet<Order>();
        }

        public int IdStatusOrder { get; set; }
        public string Nama { get; set; } = null!;
        public string Deskripsi { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
    }
}
