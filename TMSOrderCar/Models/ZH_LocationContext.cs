using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TMSOrderCar.Models
{
    public partial class ZH_LocationContext : DbContext
    {
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UserStatuses> UserStatuses { get; set; }
        public virtual DbSet<UWaybillBinding> UWaybillBinding { get; set; }

        public ZH_LocationContext(DbContextOptions<ZH_LocationContext> options)
     : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.LoginName).HasMaxLength(50);

                entity.Property(e => e.Mobile).HasMaxLength(20);

                entity.Property(e => e.Pwd).HasMaxLength(50);
            });

            modelBuilder.Entity<UserStatuses>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnType("nchar(10)");
            });

            modelBuilder.Entity<UWaybillBinding>(entity =>
            {
                entity.HasKey(e => e.DocEntry);

                entity.ToTable("U_WaybillBinding");

                entity.Property(e => e.DocEntry)
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.ActualDateLoad).HasColumnType("datetime");

                entity.Property(e => e.Carnumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreateUser)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ExpectDateLoad).HasColumnType("datetime");

                entity.Property(e => e.OrderNo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.UpdateUser)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
