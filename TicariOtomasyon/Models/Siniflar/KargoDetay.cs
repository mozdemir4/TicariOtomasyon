using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TicariOtomasyon.Models.Siniflar
{
    public class KargoDetay
    {
        [Key]
        public int KargoDetayID { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(200)]
        [Display(Name ="Açıklama")]
        public string Aciklama { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(10)]
        [Display(Name = "Takip Kodu")]
        public string TakipKodu { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string Personel { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        [Display(Name = "Alıcı")]
        public string Alici { get; set; }
        public DateTime Tarih { get; set; }
    }
}