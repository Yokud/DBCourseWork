using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace DataBaseUI
{
    public partial class SpsrLtDbContext : DbContext
    {
        public SpsrLtDbContext()
        {
        }

        public SpsrLtDbContext(DbContextOptions<SpsrLtDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Availability> Availabilities { get; set; } = null!;
        public virtual DbSet<Cost> Costs { get; set; } = null!;
        public virtual DbSet<CostStory> CostStories { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<SaleReceipt> SaleReceipts { get; set; } = null!;
        public virtual DbSet<SaleReceiptPosition> SaleReceiptPositions { get; set; } = null!;
        public virtual DbSet<Shop> Shops { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
                string connectionString = config.GetConnectionString("AdminConnection");

                optionsBuilder.UseNpgsql(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Availability>(entity =>
            {
                entity.ToTable("availability");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Productid).HasColumnName("productid");

                entity.Property(e => e.Shopid).HasColumnName("shopid");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Availabilities)
                    .HasForeignKey(d => d.Productid)
                    .HasConstraintName("availability_productid_fkey");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Availabilities)
                    .HasForeignKey(d => d.Shopid)
                    .HasConstraintName("availability_shopid_fkey");
            });

            modelBuilder.Entity<Cost>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("costs");

                entity.Property(e => e.Availabilityid).HasColumnName("availabilityid");

                entity.Property(e => e.Cost1).HasColumnName("cost");
            });

            modelBuilder.Entity<CostStory>(entity =>
            {
                entity.ToTable("coststory");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Availabilityid).HasColumnName("availabilityid");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.Month).HasColumnName("month");

                entity.Property(e => e.Year).HasColumnName("year");

                entity.HasOne(d => d.Availability)
                    .WithMany(p => p.Coststories)
                    .HasForeignKey(d => d.Availabilityid)
                    .HasConstraintName("coststory_availabilityid_fkey");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Producttype).HasColumnName("producttype");
            });

            modelBuilder.Entity<SaleReceipt>(entity =>
            {
                entity.ToTable("salereceipts");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Dateofpurchase).HasColumnName("dateofpurchase");

                entity.Property(e => e.Fio).HasColumnName("fio");

                entity.Property(e => e.Shopid).HasColumnName("shopid");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Salereceipts)
                    .HasForeignKey(d => d.Shopid)
                    .HasConstraintName("salereceipts_shopid_fkey");
            });

            modelBuilder.Entity<SaleReceiptPosition>(entity =>
            {
                entity.ToTable("salereceiptpositions");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Availabilityid).HasColumnName("availabilityid");

                entity.Property(e => e.Salereceiptid).HasColumnName("salereceiptid");

                entity.HasOne(d => d.Availability)
                    .WithMany(p => p.Salereceiptpositions)
                    .HasForeignKey(d => d.Availabilityid)
                    .HasConstraintName("salereceiptpositions_availabilityid_fkey");

                entity.HasOne(d => d.Salereceipt)
                    .WithMany(p => p.Salereceiptpositions)
                    .HasForeignKey(d => d.Salereceiptid)
                    .HasConstraintName("salereceiptpositions_salereceiptid_fkey");
            });

            modelBuilder.Entity<Shop>(entity =>
            {
                entity.ToTable("shops");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
