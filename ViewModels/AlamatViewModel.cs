using System.ComponentModel.DataAnnotations;
using marketplace.Datas.Entities;

namespace marketplace.ViewModels
{
    public class AlamatViewModel
    {
        public AlamatViewModel()
        {
            Provinsi = string.Empty;
            Kota = string.Empty;
            Kecamatan = string.Empty;
            Desa = string.Empty;
            KodePos = string.Empty;
            Deskripsi = string.Empty;
        }
        public int IdAlamat { get; set; }
        public int IdPembeli { get; set; }
        public string Provinsi { get; set; }
        public string Kota { get; set; }
        public string Kecamatan { get; set; }
        public string Desa { get; set; }
        public string KodePos { get; set; }
        public string Deskripsi { get; set; }
        
        public Alamat ConvertToDbModel(){
            return new Alamat {
                IdAlamat = this.IdAlamat,
                IdPembeli = this.IdPembeli,
                Provinsi = this.Provinsi,
                Kota = this.Kota,
                Kecamatan = this.Kecamatan,
                Desa = this.Desa,
                KodePos = this.KodePos,
                Deskripsi = this.Deskripsi,
            };
        }

        public AlamatViewModel(Alamat item){
            IdAlamat = this.IdAlamat;
            IdPembeli = this.IdPembeli;
            Provinsi = this.Provinsi;
            Kota = this.Kota;
            Kecamatan = this.Kecamatan;
            Desa = this.Desa;
            KodePos = this.KodePos;
            Deskripsi = this.Deskripsi;
        }
    }
}