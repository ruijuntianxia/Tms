using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TmsApi.Models
{
    public partial class ZH_LocationContext : DbContext
    {
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserStatuse> UserStatuse { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer(@"Server=192.168.1.193;Database=ZH_Location;User Id=zhlosa;Password=123;");
//            }
//        }
        public ZH_LocationContext(DbContextOptions<ZH_LocationContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.LoginName).HasMaxLength(50);

                entity.Property(e => e.Mobile).HasMaxLength(20);

                entity.Property(e => e.Pwd).HasMaxLength(50);
            });

            modelBuilder.Entity<UserStatuse>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnType("nchar(10)");
            });
        }
    }
}
