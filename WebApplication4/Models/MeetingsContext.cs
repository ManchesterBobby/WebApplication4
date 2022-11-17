using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication4.Models;

public partial class MeetingsContext : DbContext
{
    public MeetingsContext()
    {
    }

    public MeetingsContext(DbContextOptions<MeetingsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Meeting> Meetings { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<ScheduledMeeting> ScheduledMeetings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=LAPTOP-3B9TJ6D1\\SQLEXPRESS;Database=MeetingsTechnicalTest;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.EmployeeId).ValueGeneratedNever();
            entity.Property(e => e.Description).IsFixedLength();
        });

        modelBuilder.Entity<Meeting>(entity =>
        {
            entity.Property(e => e.Description).IsFixedLength();

            entity.HasOne(d => d.Room).WithMany(p => p.Meetings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rooms_Meetings");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.Property(e => e.RoomId).ValueGeneratedNever();
            entity.Property(e => e.Description).IsFixedLength();
        });

        modelBuilder.Entity<ScheduledMeeting>(entity =>
        {
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
