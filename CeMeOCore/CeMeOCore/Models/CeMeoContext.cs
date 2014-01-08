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
            modelBuilder.Entity<MeetingUser>()
                .HasKey(mu => new { mu.MeetingId, mu.UserId });

            //modelBuilder.Entity<UserProfile>()
            //    .HasMany(u => u.MeetingUser)
            //    .WithRequired()
            //    .HasForeignKey(mu => mu.UserId);

            //modelBuilder.Entity<Meeting>()
            //    .HasMany(m => m.MeetingUser)
            //    .WithRequired()
            //    .HasForeignKey(mu => mu.MeetingId);

            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<CeMeOCore.Models.EditLocation> EditLocations { get; set; }
    }
}