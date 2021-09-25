using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;
using System.Web;

namespace TicariOtomasyon.Models.Siniflar
{
    public class KargoTakip
    {
        [Key]
        public int KargoTakipID { get; set; }

        [Column(TypeName ="Varchar")]
        [StringLength(10)]
        [Display(Name = "Takip Kodu")]
        public string TakipKodu { get; set; }

        [Column(TypeName ="Varchar")]
        [StringLength(150)]
        [Display(Name = "Açıklama")]
        public string Aciklama { get; set; }
            
        public DateTime Tarih { get; set; }
    }
}