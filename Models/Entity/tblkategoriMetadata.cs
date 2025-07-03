using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVCSTOK.Models.Entity
{
    [MetadataType(typeof(tblkategoriMetadata))]
    public partial class tblkategori
    {
        // Bu dosya senin kontrolünde! EF bunu silmez.
    }

    public class tblkategoriMetadata
    {
        [Required(ErrorMessage = "Kategori adı boş bırakılamaz.")]
        [StringLength(50, ErrorMessage = "Kategori adı en fazla 50 karakter olabilir.")]
        public string KategoriAd { get; set; }


        [Required(ErrorMessage = "Açıklama boş bırakılamaz.")]
        [StringLength(200, ErrorMessage = "Açıklama en fazla 200 karakter olabilir.")]
        public string Aciklama { get; set; }

    }
}