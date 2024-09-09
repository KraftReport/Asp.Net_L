using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace hub_app.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AppMember> AppMembers { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<PlateCurrency> PlateCurrencies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=KRAFTREPORT;Initial Catalog=payment;User ID=sa;Password=********;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False;INTEGRATED SECURITY=SSPI");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppMember>(entity =>
        {
            entity.ToTable("app_member");
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.ToTable("currency");

            entity.Property(e => e.PlateType).HasMaxLength(24);
        });

        modelBuilder.Entity<PlateCurrency>(entity =>
        {
            entity.ToTable("PlateCurrency");

            entity.Property(e => e.PlateType).HasMaxLength(24);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
