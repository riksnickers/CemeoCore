using CeMeOCore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CeMeOCore.DAL.Context
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
        //public DbSet<MeetingUser> MeetingUsers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Calendar> Calendars{ get; set; }
        public DbSet<GuestUser> GuestUsers { get; set; }
        public DbSet<Invitee> Invitees { get; set; }
        public DbSet<Proposition> Propositions { get; set; }
        public DbSet<OrganiserProcess> OrganiserProcesses { get; set; }
        public DbSet<Attendee> Attendees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<MeetingUser>()
            //    .HasKey(mu => new { mu.MeetingId, mu.UserId });

            modelBuilder.Entity<Attendee>()
                .HasKey(a => new { a.MeetingId, a.UserId });

            modelBuilder.Entity<Attendee>()
                .HasMany(a => a.Meetings)
                .WithMany(m => m.Attendees);

            modelBuilder.Entity<Attendee>()
                .HasMany(a => a.Users)
                .WithMany(u => u.Attendees);

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
    }
}