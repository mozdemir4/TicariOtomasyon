using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TicariOtomasyon.Models.Siniflar
{
    public class Personel
    {
        [Key]
        public int PersonelID { get; set; }

        [Display(Name = "Personel Ad")]
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        [Required(ErrorMessage ="Bu alan boş geçilemez")]
        public string PersonelAd { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        public string PersonelSoyad { get; set; }

        [Column(TypeName ="Varchar")]
        [StringLength(200)]
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        public string Adress { get; set; }

        [Column(TypeName ="Varchar")]
        [StringLength(11)]
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        public string Telefon { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(250)]
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        public string PersonelGorsel { get; set; }

        public ICollection<SatisHareket> SatisHarekets { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        public int Departmanid { get; set; }
        public virtual Departman Departman { get; set; }
    }
}