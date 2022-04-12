namespace marketplace.Controllers
{
    public class UlasanRequestViewModel
    {
        public int IdOrder { get; set; }
        public string Komentar { get; set; } = null!;
        public IFormFile Gambar { get; set; } = null!;
        public int Rating { get; set; }
    }
}