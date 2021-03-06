namespace marketplace.ViewModels
{
    public class PembayaranViewModel
    {
        public PembayaranViewModel()
        {
        }

        public int IdPembayaran { get; set; }
        public DateTime TglBayar { get; set; }
        public decimal JmlBayar { get; set; }
        public decimal Pajak { get; set; }
        public string Metode { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string? Note { get; set; }
        public string Tujuan { get; set; } = null!;
        public string? FileBuktiBayar { get; set; }
    }
}