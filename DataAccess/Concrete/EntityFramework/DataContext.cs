
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class DataContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
             optionsBuilder.UseSqlServer(@"Server=DESKTOP-EQ4AUPM\SQLEXPRESS;Database=den7;Trusted_Connection=true;TrustServerCertificate=True; Integrated Security=SSPI;");
            // optionsBuilder.UseSqlServer(@"Server=DESKTOP-EQ4AUPM\SQLEXPRESS;Database=Recapnewdatabase;Trusted_Connection=true;Integrated Security=SSPI;");
            //TrustServerCertificate=True;
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Share> Shares { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.PasswordSalt)
                      .HasColumnType("varbinary(500)");

                entity.Property(e => e.PasswordHash)
                      .HasColumnType("varbinary(500)");
            });

            modelBuilder.Entity<Book>()
            .HasMany(b => b.Notes)
            .WithOne(n => n.Book)
            .HasForeignKey(n => n.BookId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Notes)
                .WithOne(n => n.User)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);
           


            modelBuilder.Entity<Note>()
           .HasMany(n => n.Shares)
           .WithOne(ns => ns.Note)
           .HasForeignKey(ns => ns.NoteId)
           .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Share>()
                .HasOne(ns => ns.SharedWithUser)
                .WithMany()
                .HasForeignKey(ns => ns.SharedWithUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Diğer ilişkiler ve konfigürasyonlar buraya eklenebilir
            base.OnModelCreating(modelBuilder);
        }

    }
}
/*
  Add-Migration InitialCreate
update-database "InitialCreate"
*/