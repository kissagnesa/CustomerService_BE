using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

public partial class CustomerserviceContext : DbContext
{
    public CustomerserviceContext()
    {
    }

    public CustomerserviceContext(DbContextOptions<CustomerserviceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Agent> Agents { get; set; }

    public virtual DbSet<Call> Calls { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("SERVER=localhost;PORT=3306;DATABASE=customerservice;USER=root;PASSWORD=;SSL MODE=none;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agent>(entity =>
        {
            entity.HasKey(e => e.AgentId).HasName("PRIMARY");

            entity.ToTable("agent");

            entity.Property(e => e.AgentId)
                .HasColumnType("int(11)")
                .HasColumnName("agent_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Call>(entity =>
        {
            entity.HasKey(e => e.CallId).HasName("PRIMARY");

            entity.ToTable("calls");

            entity.HasIndex(e => new { e.AgentId, e.CustomerId }, "agent_id");

            entity.HasIndex(e => e.CustomerId, "customer_id");

            entity.Property(e => e.CallId)
                .HasColumnType("int(11)")
                .HasColumnName("call_id");
            entity.Property(e => e.AgentId)
                .HasColumnType("int(11)")
                .HasColumnName("agent_id");
            entity.Property(e => e.CallDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("'current_timestamp()'")
                .HasColumnType("timestamp")
                .HasColumnName("call_date");
            entity.Property(e => e.CallTime)
                .HasColumnType("time")
                .HasColumnName("call_time");
            entity.Property(e => e.CustomerId)
                .HasColumnType("int(11)")
                .HasColumnName("customer_id");
            entity.Property(e => e.PhoneNumber)
                .HasColumnType("int(9)")
                .HasColumnName("phone_number");

            entity.HasOne(d => d.Agent).WithMany(p => p.Calls)
                .HasForeignKey(d => d.AgentId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("calls_ibfk_2");

            entity.HasOne(d => d.Customer).WithMany(p => p.Calls)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("calls_ibfk_1");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PRIMARY");

            entity.ToTable("customer");

            entity.Property(e => e.CustomerId)
                .HasColumnType("int(11)")
                .HasColumnName("customer_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
