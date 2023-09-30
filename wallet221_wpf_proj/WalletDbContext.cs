using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace wallet221_wpf_proj;

public partial class WalletDbContext : DbContext
{
    public WalletDbContext()
    {
    }

    public WalletDbContext(DbContextOptions<WalletDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ExchangeRateList> ExchangeRateLists { get; set; }

    public virtual DbSet<RublesCard> RublesCards { get; set; }

    public virtual DbSet<RublesDeposit> RublesDeposits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-NLBVKIFJ\\SQLEXPRESS;Initial Catalog=WalletDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Client__3214EC07C687174D");

            entity.ToTable("Client");

            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.SurName).HasMaxLength(100);
        });

        modelBuilder.Entity<ExchangeRateList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Exchange__3214EC0707432E46");

            entity.ToTable("ExchangeRateList");

            entity.Property(e => e.CurrencyName).HasMaxLength(25);
        });

        modelBuilder.Entity<RublesCard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RublesCa__3214EC0705EFF6B4");

            entity.ToTable("RublesCard");

            entity.Property(e => e.CardBalance).HasColumnType("money");
        });

        modelBuilder.Entity<RublesDeposit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RublesDe__3214EC07724F8834");

            entity.ToTable("RublesDeposit");

            entity.Property(e => e.DepositBalance).HasColumnType("money");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
