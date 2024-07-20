using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data;

namespace Shoe_Store.Models
{
    public class Shoe_Store_DbContext: DbContext
    {
        public Shoe_Store_DbContext(DbContextOptions<Shoe_Store_DbContext> options):base(options)
        { 
        }

        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<Loai> loais { get; set; }
        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<NhaCungcap> NhaCungCaps { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<DanhGia> DanhGia { get; set; }
        public DbSet<SanPhamChitiet> sanPhamChitiets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SanPham>()
           .HasKey(s => s.SanPhamId);

            modelBuilder.Entity<SanPham>()
                .HasOne(s => s.NhaCungcap)
                .WithMany(nc => nc.SanPhams)
                .HasForeignKey(s => s.idnhacungcap)
                .OnDelete(DeleteBehavior.Restrict);

            // Cấu hình cho bảng SanPhamChitiet
            modelBuilder.Entity<SanPhamChitiet>()
                .HasKey(spct => spct.SanPhamId);

            modelBuilder.Entity<SanPhamChitiet>()
                .HasOne(spct => spct.SanPham)
                .WithMany(sp => sp.sanPhamChitiets)
                .HasForeignKey(spct => spct.SanPhamId)
                .OnDelete(DeleteBehavior.Cascade);

            // Cấu hình cho bảng Loai
            modelBuilder.Entity<Loai>()
                .HasKey(l => l.Id);

            modelBuilder.Entity<Loai>()
                .HasMany(l => l.SanPhams)
                .WithMany(sp => sp.Loais)
                .UsingEntity(j => j.ToTable("SanPhamLoai")); // Tạo bảng liên kết trung gian

            // Cấu hình cho bảng DanhGia
            modelBuilder.Entity<DanhGia>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<DanhGia>()
                .HasOne(d => d.SanPham)
                .WithMany(sp => sp.DanhGias)
                .HasForeignKey(d => d.SanPhamId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DanhGia>()
                .HasOne(d => d.NguoiDung)
                .WithMany(nd => nd.DanhGias)
                .HasForeignKey(d => d.NguoiDungId)
                .OnDelete(DeleteBehavior.Restrict);

            // Cấu hình cho bảng DonHang
            modelBuilder.Entity<DonHang>()
                .HasKey(dh => dh.Id);

            modelBuilder.Entity<DonHang>()
                .HasOne(dh => dh.NguoiDung)
                .WithMany(nd => nd.DonHangs)
                .HasForeignKey(dh => dh.NguoiDungId)
                .OnDelete(DeleteBehavior.Cascade);

            // Cấu hình cho bảng NguoiDung
            modelBuilder.Entity<NguoiDung>()
                .HasKey(nd => nd.Id);

            // Cấu hình cho bảng NhaCungcap
            modelBuilder.Entity<NhaCungcap>()
                .HasKey(nc => nc.Id);

            // Cấu hình cho bảng Role
            modelBuilder.Entity<Roles>()
                .HasKey(r => r.RoleId);
        }
    }
}

