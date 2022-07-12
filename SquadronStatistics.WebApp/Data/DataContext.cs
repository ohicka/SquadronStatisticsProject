using Microsoft.EntityFrameworkCore;
using SquadronStatistics.WebApp.Data.Entities;
using System;

namespace SquadronStatistics.WebApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<File> Files { get; set; }
        public DbSet<FileRecord> FileRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<File>(entity =>
            {
                entity.Property<DateTime>("UploadDate")
                .HasColumnType("smalldatetime")
                .HasDefaultValueSql("(getdate())");
            });
        }
    }
}
