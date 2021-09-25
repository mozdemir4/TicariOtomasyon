using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TicariOtomasyon.Models.Siniflar
{
    public class Cari
    {
        [Key]
        public int CariID { get; set; }

        [Display(Name = "Cari Ad")]
        [Column(TypeName = "varchar")]
        [StringLength(30, ErrorMessage ="En fazla 30 karakter girebilirsiniz!!!")]
        [Required(ErrorMessage ="Bu alanı boş geçemessiniz!!!")]
        public string CariAd { get; set; }

        [Display(Name = "Cari Soyad")]
        [Column(TypeName = "varchar")]
        [StringLength(30)]
        [Required(ErrorMessage = "Bu alanı boş geçemessiniz!!!")]
        public string CariSoyad { get; set; }

        [Display(Name = "Cari Şehir")]
        [Column(TypeName = "varchar")]
        [StringLength(14)]
        public string CariSehir { get; set; }

        [Display(Name = "Cari Mail")]
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        [RegularExpression(".+\\@.+\\..+")]
        public string CariMail { get; set; }

        [Display(Name = "Cari Telefon")]
        [Column(TypeName = "varchar")]
        [StringLength(11)]
        public string CariTelefon { get; set; }

        [Display(Name = "Cari Şifre")]
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        [Required(ErrorMessage = "Bu alanı boş geçemessiniz!!!")]
        public string CariSifre { get; set; }

        public bool Durum { get; set; }

        public ICollection<SatisHareket> SatisHarekets { get; set; }
    }
}