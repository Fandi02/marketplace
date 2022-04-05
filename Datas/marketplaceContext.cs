using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using marketplace.Datas.Entities;

namespace marketplace.Datas
{
    public partial class marketplaceContext : DbContext
    {
        public marketplaceContext()
        {
        }

        public marketplaceContext(DbContextOptions<marketplaceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Alamat> Alamats { get; set; } = null!;
        public virtual DbSet<DetailOrder> DetailOrders { get; set; } = null!;
        public virtual DbSet<Kategori> Kategoris { get; set; } = null!;
        public virtual DbSet<Keranjang> Keranjangs { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Pembayaran> Pembayarans { get; set; } = null!;
        public virtual DbSet<Pembeli> Pembelis { get; set; } = null!;
        public virtual DbSet<Pengiriman> Pengirimen { get; set; } = null!;
        public virtual DbSet<Produk> Produks { get; set; } = null!;
        public virtual DbSet<ProdukKategori> ProdukKategoris { get; set; } = null!;
        public virtual DbSet<StatusOrder> StatusOrders { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;user=root;database=marketplace", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.13-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.IdAdmin)
                    .HasName("PRIMARY");

                entity.ToTable("admin");

                entity.Property(e => e.IdAdmin)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_admin");

                entity.Property(e => e.Email)
                    .HasMaxLength(25)
                    .HasColumnName("email");

                entity.Property(e => e.Nama)
                    .HasMaxLength(25)
                    .HasColumnName("nama");

                entity.Property(e => e.NoHp)
                    .HasMaxLength(13)
                    .HasColumnName("no_hp");

                entity.Property(e => e.Password)
                    .HasMaxLength(15)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .HasMaxLength(25)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Alamat>(entity =>
            {
                entity.HasKey(e => e.IdAlamat)
                    .HasName("PRIMARY");

                entity.ToTable("alamat");

                entity.HasIndex(e => e.IdPembeli, "id_pembeli");

                entity.Property(e => e.IdAlamat)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_alamat");

                entity.Property(e => e.Desa)
                    .HasMaxLength(25)
                    .HasColumnName("desa");

                entity.Property(e => e.Deskripsi)
                    .HasMaxLength(25)
                    .HasColumnName("deskripsi");

                entity.Property(e => e.IdPembeli)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_pembeli");

                entity.Property(e => e.Kecamatan)
                    .HasMaxLength(25)
                    .HasColumnName("kecamatan");

                entity.Property(e => e.KodePos)
                    .HasMaxLength(5)
                    .HasColumnName("kode_pos");

                entity.Property(e => e.Kota)
                    .HasMaxLength(25)
                    .HasColumnName("kota");

                entity.Property(e => e.Provinsi)
                    .HasMaxLength(25)
                    .HasColumnName("provinsi");
            });

            modelBuilder.Entity<DetailOrder>(entity =>
            {
                entity.ToTable("detail_order");

                entity.HasIndex(e => e.IdOrder, "id_order");

                entity.HasIndex(e => e.IdProduk, "id_produk");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Harga)
                    .HasPrecision(10)
                    .HasColumnName("harga");

                entity.Property(e => e.IdOrder)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_order");

                entity.Property(e => e.IdProduk)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_produk");

                entity.Property(e => e.JmlBarang)
                    .HasColumnType("int(10)")
                    .HasColumnName("jml_barang");

                entity.Property(e => e.SubTotal)
                    .HasPrecision(10)
                    .HasColumnName("sub_total");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.DetailOrders)
                    .HasForeignKey(d => d.IdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("detail_order_ibfk_1");

                entity.HasOne(d => d.IdProdukNavigation)
                    .WithMany(p => p.DetailOrders)
                    .HasForeignKey(d => d.IdProduk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("detail_order_ibfk_2");
            });

            modelBuilder.Entity<Kategori>(entity =>
            {
                entity.HasKey(e => e.IdKategori)
                    .HasName("PRIMARY");

                entity.ToTable("kategori");

                entity.Property(e => e.IdKategori)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_kategori");

                entity.Property(e => e.Deskripsi)
                    .HasMaxLength(255)
                    .HasColumnName("deskripsi");

                entity.Property(e => e.Icon)
                    .HasMaxLength(255)
                    .HasColumnName("icon");

                entity.Property(e => e.Nama)
                    .HasMaxLength(25)
                    .HasColumnName("nama");
            });

            modelBuilder.Entity<Keranjang>(entity =>
            {
                entity.HasKey(e => e.IdKeranjang)
                    .HasName("PRIMARY");

                entity.ToTable("keranjang");

                entity.HasIndex(e => e.IdPembeli, "id_pembeli");

                entity.HasIndex(e => e.IdProduk, "id_produk");

                entity.Property(e => e.IdKeranjang)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_keranjang");

                entity.Property(e => e.IdPembeli)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_pembeli");

                entity.Property(e => e.IdProduk)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_produk");

                entity.Property(e => e.JmlBarang)
                    .HasColumnType("int(11)")
                    .HasColumnName("jml_barang");

                entity.Property(e => e.SubTotal)
                    .HasPrecision(25, 2)
                    .HasColumnName("sub_total");

                entity.HasOne(d => d.IdPembeliNavigation)
                    .WithMany(p => p.Keranjangs)
                    .HasForeignKey(d => d.IdPembeli)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("keranjang_ibfk_2");

                entity.HasOne(d => d.IdProdukNavigation)
                    .WithMany(p => p.Keranjangs)
                    .HasForeignKey(d => d.IdProduk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("keranjang_ibfk_1");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.IdOrder)
                    .HasName("PRIMARY");

                entity.ToTable("order");

                entity.HasIndex(e => e.IdAlamat, "id_alamat");

                entity.HasIndex(e => e.IdKeranjang, "id_keranjang");

                entity.HasIndex(e => e.IdPembeli, "id_pembeli");

                entity.HasIndex(e => e.IdStatusOrder, "id_status_order");

                entity.Property(e => e.IdOrder)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_order");

                entity.Property(e => e.IdAlamat)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_alamat");

                entity.Property(e => e.IdKeranjang)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_keranjang");

                entity.Property(e => e.IdPembeli)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_pembeli");

                entity.Property(e => e.IdStatusOrder)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_status_order");

                entity.Property(e => e.JmlBayar)
                    .HasPrecision(25)
                    .HasColumnName("jml_bayar");

                entity.Property(e => e.Notes)
                    .HasMaxLength(25)
                    .HasColumnName("notes");

                entity.Property(e => e.TglTransaksi).HasColumnName("tgl_transaksi");

                entity.HasOne(d => d.IdAlamatNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdAlamat)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_ibfk_3");

                entity.HasOne(d => d.IdKeranjangNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdKeranjang)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_ibfk_1");

                entity.HasOne(d => d.IdPembeliNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdPembeli)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_ibfk_2");

                entity.HasOne(d => d.IdStatusOrderNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdStatusOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_ibfk_4");
            });

            modelBuilder.Entity<Pembayaran>(entity =>
            {
                entity.HasKey(e => e.IdPembayaran)
                    .HasName("PRIMARY");

                entity.ToTable("pembayaran");

                entity.HasIndex(e => e.IdOrder, "id_order");

                entity.HasIndex(e => e.IdPembeli, "id_pembeli");

                entity.Property(e => e.IdPembayaran)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_pembayaran");

                entity.Property(e => e.IdOrder)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_order");

                entity.Property(e => e.IdPembeli)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_pembeli");

                entity.Property(e => e.JmlBayar)
                    .HasPrecision(25)
                    .HasColumnName("jml_bayar");

                entity.Property(e => e.MetodePembayaran)
                    .HasMaxLength(25)
                    .HasColumnName("metode_pembayaran");

                entity.Property(e => e.Pajak)
                    .HasPrecision(25)
                    .HasColumnName("pajak");

                entity.Property(e => e.TglBayar).HasColumnName("tgl_bayar");

                entity.Property(e => e.Tujuan)
                    .HasMaxLength(255)
                    .HasColumnName("tujuan");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.Pembayarans)
                    .HasForeignKey(d => d.IdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pembayaran_ibfk_2");

                entity.HasOne(d => d.IdPembeliNavigation)
                    .WithMany(p => p.Pembayarans)
                    .HasForeignKey(d => d.IdPembeli)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pembayaran_ibfk_1");
            });

            modelBuilder.Entity<Pembeli>(entity =>
            {
                entity.HasKey(e => e.IdPembeli)
                    .HasName("PRIMARY");

                entity.ToTable("pembeli");

                entity.Property(e => e.IdPembeli)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_pembeli");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Foto)
                    .HasMaxLength(255)
                    .HasColumnName("foto");

                entity.Property(e => e.IsAdmin)
                    .HasMaxLength(15)
                    .HasColumnName("is_admin");

                entity.Property(e => e.Nama)
                    .HasMaxLength(25)
                    .HasColumnName("nama");

                entity.Property(e => e.NoHp)
                    .HasMaxLength(13)
                    .HasColumnName("no_hp");

                entity.Property(e => e.Password)
                    .HasMaxLength(15)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .HasMaxLength(25)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Pengiriman>(entity =>
            {
                entity.HasKey(e => e.IdPengiriman)
                    .HasName("PRIMARY");

                entity.ToTable("pengiriman");

                entity.HasIndex(e => e.IdAlamat, "id_alamat");

                entity.HasIndex(e => e.IdOrder, "id_order");

                entity.Property(e => e.IdPengiriman)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_pengiriman");

                entity.Property(e => e.IdAlamat)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_alamat");

                entity.Property(e => e.IdOrder)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_order");

                entity.Property(e => e.Kurir)
                    .HasColumnType("int(11)")
                    .HasColumnName("kurir");

                entity.Property(e => e.Ongkir)
                    .HasColumnType("int(11)")
                    .HasColumnName("ongkir");

                entity.HasOne(d => d.IdAlamatNavigation)
                    .WithMany(p => p.Pengirimen)
                    .HasForeignKey(d => d.IdAlamat)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pengiriman_ibfk_2");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.Pengirimen)
                    .HasForeignKey(d => d.IdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pengiriman_ibfk_1");
            });

            modelBuilder.Entity<Produk>(entity =>
            {
                entity.HasKey(e => e.IdProduk)
                    .HasName("PRIMARY");

                entity.ToTable("produk");

                entity.Property(e => e.IdProduk)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_produk");

                entity.Property(e => e.Deskripsi)
                    .HasMaxLength(100)
                    .HasColumnName("deskripsi");

                entity.Property(e => e.Gambar)
                    .HasMaxLength(255)
                    .HasColumnName("gambar");

                entity.Property(e => e.Harga)
                    .HasPrecision(10)
                    .HasColumnName("harga");

                entity.Property(e => e.Nama)
                    .HasMaxLength(25)
                    .HasColumnName("nama");

                entity.Property(e => e.Stok)
                    .HasColumnType("int(4)")
                    .HasColumnName("stok");
            });

            modelBuilder.Entity<ProdukKategori>(entity =>
            {
                entity.HasKey(e => e.IdProdukKategori)
                    .HasName("PRIMARY");

                entity.ToTable("produk_kategori");

                entity.HasIndex(e => e.IdKategori, "id_kategori");

                entity.HasIndex(e => e.IdProduk, "id_produk");

                entity.Property(e => e.IdProdukKategori)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_produk_kategori");

                entity.Property(e => e.IdKategori)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_kategori");

                entity.Property(e => e.IdProduk)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_produk");

                entity.HasOne(d => d.IdKategoriNavigation)
                    .WithMany(p => p.ProdukKategoris)
                    .HasForeignKey(d => d.IdKategori)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("produk_kategori_ibfk_2");

                entity.HasOne(d => d.IdProdukNavigation)
                    .WithMany(p => p.ProdukKategoris)
                    .HasForeignKey(d => d.IdProduk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("produk_kategori_ibfk_1");
            });

            modelBuilder.Entity<StatusOrder>(entity =>
            {
                entity.HasKey(e => e.IdStatusOrder)
                    .HasName("PRIMARY");

                entity.ToTable("status_order");

                entity.Property(e => e.IdStatusOrder)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_status_order");

                entity.Property(e => e.Deskripsi)
                    .HasMaxLength(255)
                    .HasColumnName("deskripsi");

                entity.Property(e => e.Nama)
                    .HasMaxLength(25)
                    .HasColumnName("nama");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
