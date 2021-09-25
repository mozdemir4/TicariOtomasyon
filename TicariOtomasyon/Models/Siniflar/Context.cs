using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace TicariOtomasyon.Models.Siniflar
{
    public class Context : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Cari> Caris { get; set; }
        public DbSet<Departman> Departmen { get; set; }
        public DbSet<FaturaKalem> FaturaKalems { get; set; }
        public DbSet<Faturalar> Faturalars { get; set; }
        public DbSet<Giderler> Giderlers { get; set; }
        public DbSet<Kategori> Kategoris { get; set; }
        public DbSet<Personel> Personels { get; set; }
        public DbSet<SatisHareket> SatisHarekets { get; set; }
        public DbSet<Urun> Uruns { get; set; }
        public DbSet<UrunDetay> urunDetays { get; set; }
        public DbSet<Yapilacak> yapilacaks { get; set; }
        public DbSet<KargoDetay> KargoDetays { get; set; }
        public DbSet<KargoTakip> KargoTakips { get; set; }
        public DbSet<Mesajlar> Mesajlars { get; set; }
    }
}