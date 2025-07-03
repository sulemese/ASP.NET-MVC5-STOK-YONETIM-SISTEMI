using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MVCSTOK.Models.Entity
{
    public class DashboardViewModel
    {
        public int UrunSayisi { get; set; }
        public int MusteriSayisi { get; set; }
        public int PersonelSayisi { get; set; }
        public decimal ToplamGelir { get; set; }
   
        public List<tblurunler> SonUrunler { get; set; }

        public List<tblurunler> KritikUrunler { get; set; }


        //grafik için
        public List<tblurunler> KategoriAdları;
        public List<tblurunler> UrunSayilari;


    }
}