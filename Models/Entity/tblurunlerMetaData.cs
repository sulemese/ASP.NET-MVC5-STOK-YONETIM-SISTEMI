using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCSTOK.Models.Entity
{
    [MetadataType(typeof(tblurunlerMetaData))]
    public partial class tblurunler 
    { 
    
    }
    public class tblurunlerMetaData
    {
        [Required(ErrorMessage = "Ürün adı gereklidir.")]
        [StringLength(100, ErrorMessage = "Ürün adı en fazla 100 karakter olabilir.")]
        [Display(Name = "Ürün Adı")]
        public string UrunAd { get; set; }

        [Required(ErrorMessage = "Marka adı gereklidir.")]
        [StringLength(50, ErrorMessage = "Marka en fazla 50 karakter olabilir.")]
        [Display(Name = "Marka")]
        public string marka { get; set; }

        [Required(ErrorMessage = "Stok adedi gereklidir.")]
        [Range(0, int.MaxValue, ErrorMessage = "Stok adedi negatif olamaz.")]
        [Display(Name = "Stok Adedi")]
        public int? StokAdet { get; set; }

        [Required(ErrorMessage = "Alış fiyatı gereklidir.")]
        [Range(0.01, 1000000, ErrorMessage = "Geçerli bir alış fiyatı giriniz.")]
        [Display(Name = "Alış Fiyatı")]
        public decimal? alisfiyat { get; set; }

        [Required(ErrorMessage = "Satış fiyatı gereklidir.")]
        [Range(0.01, 1000000, ErrorMessage = "Geçerli bir satış fiyatı giriniz.")]
        [Display(Name = "Satış Fiyatı")]
        public decimal? satisfiyat { get; set; }

        [Required(ErrorMessage = "Kategori seçimi gereklidir.")]
        [Display(Name = "Kategori")]
        public int? KategoriId { get; set; }

        [Display(Name = "Aktif mi?")]
        public bool? aktiflik { get; set; }

        [Display(Name = "Eklenme Tarihi")]
        public DateTime? EklenmeTarihi { get; set; }
    }
}