using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCSTOK.Models.Entity
{
    [MetadataType(typeof(tblmusteriMetaData))]
    public partial class tblmusteri
    {
        // Metadata ile ilişkilendiriliyor (boş bırakılır)
    }

    public class tblmusteriMetaData
    {
        [Display(Name = "Müşteri ID")]
        public int MüsteriId { get; set; }

        [Required(ErrorMessage = "Müşteri adı zorunludur.")]
        [StringLength(50, ErrorMessage = "En fazla 50 karakter olabilir.")]
        [Display(Name = "Ad")]
        public string MusteriAd { get; set; }

        [Required(ErrorMessage = "Müşteri soyadı zorunludur.")]
        [StringLength(50, ErrorMessage = "En fazla 50 karakter olabilir.")]
        [Display(Name = "Soyad")]
        public string MusteriSoyad { get; set; }
        
        [Required(ErrorMessage = "Şehir zorunludur.")]
        [Display(Name = "Şehir")]
        [StringLength(30)]
        public string sehir { get; set; }
        
        [Required(ErrorMessage = "Bakiye zorunludur.")]
        [Display(Name = "Bakiye")]
        [Range(0, 100000, ErrorMessage = "Bakiye 0 ile 100000 arasında olmalıdır.")]
        public Nullable<decimal> bakiye { get; set; }

        public Nullable<bool> aktiflik { get; set; }

        [Required(ErrorMessage = "E-posta zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta giriniz.")]
        [Display(Name = "E-Posta")]
        public string MusteriEmail { get; set; }

        [Required(ErrorMessage = "Telefon numarası  zorunludur.")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        [Display(Name = "Telefon")]
        public string MusteriTelefon { get; set; }

        [Required(ErrorMessage = "Adres zorunludur.")]
        [Display(Name = "Adres")]
        [StringLength(200)]
        public string MusteriAdres { get; set; }

        public Nullable<System.DateTime> MusteriEklenmeTarihi { get; set; }

    }
}