namespace CeMeOCore.Migrations
{
    using CeMeOCore.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CeMeOCore.Models.CeMeoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(CeMeOCore.Models.CeMeoContext context)
        {
            //locations


            Location HoofdkantoorHasselt = new Location
                   {
                       Name = "Hoofdkantoor Hasselt",
                       Street = "Universiteitslaan",
                       Number = "9",
                       Zip = "3500",
                       City = "Hasselt",
                       Country = "Belgium"
                   };

            Location KantoorAntwerpen = new Location
                   {
                       Name = "Kantoor Antwerpen",
                       Street = "Noorderlaan",
                       Number = "87",
                       Zip = "2030",
                       City = "Antwerpen",
                       Country = "Belgium"
                   };

            Location KantoorLeuven = new Location
                   {
                       Name = "Kantoor Leuven",
                       Street = "Interleuvenlaan",
                       Number = "16",
                       Zip = "3001",
                       City = "Leuven",
                       Country = "Belgium"
                   };

            Location KantoorWetteren = new Location
                   {
                       Name = "Axio Systems",
                       Street = "Honderdweg",
                       Number = "21",
                       Zip = "9230",
                       City = "Wetteren",
                       Country = "Belgium"
                   };

            Location KantoorLuik = new Location
                   {
                       Name = "NSI IT Software & Services – Liège",
                       Street = "Chaussée de Bruxelles",
                       Number = "174A",
                       Zip = "4340",
                       City = "Awans",
                       Country = "Belgium"
                   };

            Location KantoorBraine = new Location
                   {
                       Name = "NSI, Parc de l'Alliance",
                       Street = "Avenue de Finlande 8",
                       Number = "8",
                       Zip = "1420",
                       City = "Braine-l'Alleud",
                       Country = "Belgium"
                   };

            Location KantoorNamen = new Location
                   {
                       Name = "NSI IT Software & Services - Namur ",
                       Street = "Business Center - Parc Créalys,  Rue Camille Hubert 5 ",
                       Number = "5",
                       Zip = "5032",
                       City = "Gembloux",
                       Country = "Belgium"
                   };
            Location KantoorVeenendaal = new Location
            {
                Name = "Kantoor Veenendaal",
                Street = "Gildetrom",
                Number = "27-33/35-45",
                Zip = "3905",
                City = "Veenendaal",
                Country = "Netherlands"
            };

            context.Locations.AddOrUpdate(
                 l => l.Name,
                   KantoorVeenendaal,
                   HoofdkantoorHasselt,
                   KantoorAntwerpen,
                   KantoorLeuven,
                   KantoorWetteren,
                   KantoorLuik,
                   KantoorBraine,
                   KantoorNamen
               );

            //Rooms
            Room ConferenceRoomFirstFloor = new Room
                    {
                        Name = "Conference room first floor Hasselt",
                        Type = "Conference room for online meetings",
                        LocationID = KantoorVeenendaal,
                        BeamerPresent = true
                    };
            Room meetingRoomFirstFloor = new Room
                    {
                        Name = "Meetingroom first floor",
                        Type = "Normal meetingroom for live meetings",
                        LocationID = HoofdkantoorHasselt,
                        BeamerPresent = false
                    };
            Room MeetingRoomSecondFloor = new Room
                    {
                        Name = "Meetingroom second floor",
                        Type = "Normal meetingroom for live meetings",
                        LocationID = KantoorAntwerpen,
                        BeamerPresent = false
                    };
            Room MeetingRoomThordFloor = new Room
                    {
                        Name = "Meetingroom third floor",
                        Type = "Normal meetingroom for live meetings",
                        LocationID = KantoorBraine,
                        BeamerPresent = false
                    };
            Room ConferenceRoomSecondFloor = new Room
                    {
                        Name = "Conference room second floor",
                        Type = "Conference room for online meetings",
                        LocationID = KantoorLeuven,
                        BeamerPresent = true
                    };
            Room ConferenceRoomThirdFloor = new Room
                    {
                        Name = "Conference room third floor",
                        Type = "Conference room for online meetings",
                        LocationID = KantoorWetteren,
                        BeamerPresent = true
                    };
            Room MeetingRoomBasement = new Room
                    {
                        Name = "Meetingroom basement",
                        Type = "meetingroom for small teammeetings",
                        LocationID = KantoorLuik,
                        BeamerPresent = false
                    };

            context.Rooms.AddOrUpdate(
                  l => l.Name,
                  ConferenceRoomFirstFloor,
                  meetingRoomFirstFloor,
                  MeetingRoomSecondFloor,
                  MeetingRoomThordFloor,
                  ConferenceRoomSecondFloor,
                  ConferenceRoomThirdFloor,
                  MeetingRoomBasement
                );

           //Appointments
            /*Appointment GeneralMeeting = new Appointment
            {
                


            };
            Appointment TeamMeeting = new Appointment
            {

            };
            Appointment DuoMeeting = new Appointment
            {

            };
            Appointment GroupMeeting = new Appointment
            {

            };*/
        }
    }
}
