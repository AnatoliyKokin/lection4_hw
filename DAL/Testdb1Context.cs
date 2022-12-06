using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace lection4_hw.DAL;

public partial class Testdb1Context : DbContext
{
    public Testdb1Context()
    {
    }

    public Testdb1Context(DbContextOptions<Testdb1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Entities.Currency> Currencies { get; set; }

    public virtual DbSet<Entities.Deposit> Deposits { get; set; }

    public virtual DbSet<Entities.Person> Persons { get; set; }

    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Username=kokin;Password=02121987;Database=testdb1");*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Entities.Currency>(entity =>
        {
            entity.HasKey(e => e.ShortTitle).HasName("currencies_pkey");

            entity.ToTable("currencies");

            entity.Property(e => e.ShortTitle)
                .HasMaxLength(10)
                .HasColumnName("short_title");
            entity.Property(e => e.Country)
                .HasMaxLength(40)
                .HasColumnName("country");
            entity.Property(e => e.LongTitle)
                .HasMaxLength(20)
                .HasColumnName("long_title");
        });

        modelBuilder.Entity<Entities.Deposit>(entity =>
        {
            entity.HasKey(e => e.DepoNumber).HasName("deposits_pkey");

            entity.ToTable("deposits");

            entity.Property(e => e.DepoNumber).HasColumnName("depo_number");
            entity.Property(e => e.Balance)
                .HasPrecision(15, 2)
                .HasColumnName("balance");
            entity.Property(e => e.Currency)
                .HasMaxLength(10)
                .HasColumnName("currency");
            entity.Property(e => e.Person).HasColumnName("person");

            entity.HasOne(d => d.CurrencyNavigation).WithMany(p => p.Deposits)
                .HasForeignKey(d => d.Currency)
                .HasConstraintName("deposits_currency_fkey");

            entity.HasOne(d => d.PersonNavigation).WithMany(p => p.Deposits)
                .HasForeignKey(d => d.Person)
                .HasConstraintName("deposits_person_fkey");
        });

        modelBuilder.Entity<Entities.Person>(entity =>
        {
            entity.HasKey(e => e.Passport).HasName("persons_pkey");

            entity.ToTable("persons");

            entity.Property(e => e.Passport)
                .ValueGeneratedNever()
                .HasColumnName("passport");
            entity.Property(e => e.FirstName)
                .HasMaxLength(40)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(40)
                .HasColumnName("last_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
