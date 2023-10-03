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

    public virtual DbSet<History> Histories { get; set; }

    public virtual DbSet<RateList> RateLists { get; set; }

    public virtual DbSet<RublesCard> RublesCards { get; set; }

    public virtual DbSet<RublesDeposit> RublesDeposits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-NLBVKIFJ\\SQLEXPRESS;Initial Catalog=WalletDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Client__3214EC0744D7A031");

            entity.ToTable("Client");

            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.SurName).HasMaxLength(100);
        });

        modelBuilder.Entity<History>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__History__3214EC07EE5958E1");

            entity.ToTable("History");

            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Client).WithMany(p => p.Histories)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK_History_Client");
        });

        modelBuilder.Entity<RateList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RateList__3214EC074997552E");

            entity.ToTable("RateList");

            entity.Property(e => e.CurrencyName).HasMaxLength(25);
        });

        modelBuilder.Entity<RublesCard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RublesCa__3214EC07FA433844");

            entity.ToTable("RublesCard");

            entity.Property(e => e.CardBalance).HasColumnType("money");

            entity.HasOne(d => d.Client).WithMany(p => p.RublesCards)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK_RublesCard_Client");
        });

        modelBuilder.Entity<RublesDeposit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RublesDe__3214EC07DF71F761");

            entity.ToTable("RublesDeposit");

            entity.Property(e => e.DepositBalance).HasColumnType("money");

            entity.HasOne(d => d.Client).WithMany(p => p.RublesDeposits)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK_RublesDeposit_Client");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
