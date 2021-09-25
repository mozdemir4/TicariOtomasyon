using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TicariOtomasyon.Models.Siniflar
{
    public class UrunDetay
    {
        [Key]
        public int DetayID { get; set; }

        [Column(TypeName ="varchar")]
        [StringLength(30)]
        public string UrunAd { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(200)]
        public string UrunBilgi { get; set; }

    }
}