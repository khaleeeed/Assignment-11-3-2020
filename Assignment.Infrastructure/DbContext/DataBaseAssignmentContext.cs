using System;
using Assignment.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Infrastructure.DbContext
{
    public partial class DataBaseAssignmentContext : Microsoft.EntityFrameworkCore.DbContext , IDataBaseAssignmentContext
    {
        public DataBaseAssignmentContext()
        {
            
        }

        public DataBaseAssignmentContext(DbContextOptions<DataBaseAssignmentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AssignmentTable> AssignmentTable { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=DataBaseAssignment;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder  modelBuilder)
        {
            modelBuilder.Entity<AssignmentTable>(entity =>
            {
                entity.ToTable("Assignment.table");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Color)
                    .HasColumnName("color")
                    .HasMaxLength(500);

                entity.Property(e => e.ColorCode)
                    .HasColumnName("colorCode")
                    .HasMaxLength(750);

                entity.Property(e => e.DeliveredIn)
                    .HasColumnName("deliveredIn")
                    .HasMaxLength(750);

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(4000);

                entity.Property(e => e.DiscountPrice).HasColumnName("discountPrice");

                entity.Property(e => e.ItemCode).HasMaxLength(1500);

                entity.Property(e => e.Key)
                    .HasColumnName("key")
                    .HasMaxLength(500);

                entity.Property(e => e.Q1)
                    .HasColumnName("q1")
                    .HasMaxLength(500);

                entity.Property(e => e.Size).HasColumnName("size");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
