using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CeMeOCore.Models
{
    public class CeMeoContext : DbContext
    {
        public CeMeoContext()
            : base("DefaultConnection")
        {

        }

        public DbSet<UserProfile> Users { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<MeetingUser> MeetingUsers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Calendar> Calendars{ get; set; }
        public DbSet<GuestUser> GuestUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meeting>().
                HasMany(c => c.Members).
                WithMany(p => p.Meetings).
                Map(
                m =>
                {
                    m.MapLeftKey("MeetingID");
                    m.MapRightKey("UserID");
                    m.ToTable("MeetingUsers");
                });
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserProfile>().
                HasMany(c => c.Meetings).
                WithMany(p => p.Members).
                Map(
                m =>
                {
                    m.MapLeftKey("UserID");
                    m.MapRightKey("MeetingID");
                    m.ToTable("MeetingUsers");
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}