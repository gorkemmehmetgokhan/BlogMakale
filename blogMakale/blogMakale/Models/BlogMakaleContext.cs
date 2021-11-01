
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blogMakale.Models;


namespace blogMakale.Models
{
    public class BlogMakaleContext : DbContext
    {
        public BlogMakaleContext(DbContextOptions<BlogMakaleContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MakaleKategoriModel>().HasKey(sc => new { sc.id_Makale, sc.id_Kategori });
            modelBuilder.Entity<MakaleEtiketModel>().HasKey(sc => new { sc.id_Makale, sc.id_Etiket });
            modelBuilder.Entity<FavoriModel>().HasKey(sc => new { sc.id_Makale, sc.id_Kullanici });

            modelBuilder.Entity<MakaleFotoModel>()
            .HasOne<MakaleModel>(s => s.Makale)
            .WithMany(g => g.MakaleFoto)
            .HasForeignKey(s => s.id_Makale);

            modelBuilder.Entity<MakaleYorumModel>()
           .HasOne<MakaleModel>(s => s.Makale)
           .WithMany(g => g.MakaleYorum)
           .HasForeignKey(s => s.id_Makale).OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<MakaleYorumModel>()
            .HasOne<KullaniciModel>(s => s.Kullanici)
            .WithMany(g => g.MakaleYorum)
            .HasForeignKey(s => s.id_Kullanici).OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<MakaleModel>()
            .HasOne<KullaniciModel>(s => s.Kullanici)
            .WithMany(g => g.Makale)
            .HasForeignKey(s => s.id_Kullanici);
            
             modelBuilder.Entity<FavoriModel>()
            .HasOne(e => e.Makale)
            .WithMany(e => e.Favori)
            .HasForeignKey(e => e.id_Makale)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FavoriModel>()
           .HasOne(e => e.Kullanici)
           .WithMany(e => e.Favori)
           .HasForeignKey(e => e.id_Kullanici)
           .OnDelete(DeleteBehavior.Restrict);

        }

        public virtual DbSet<KullaniciModel> Kullanici { get; set; }
        public virtual DbSet<EtiketModel> Etiket { get; set; }
        public virtual DbSet<KategoriModel> Kategori { get; set; }
        public virtual DbSet<MakaleModel> Makale { get; set; }
        public virtual DbSet<MakaleYorumModel> MakaleYorum { get; set; }
        public virtual DbSet<MakaleEtiketModel> MakaleEtiket { get; set; }
        public virtual DbSet<MakaleKategoriModel> MakaleKategori { get; set; }
        public virtual DbSet<MakaleFotoModel> MakaleFoto { get; set; }
        public virtual DbSet<FavoriModel> Favori { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

    }
}
