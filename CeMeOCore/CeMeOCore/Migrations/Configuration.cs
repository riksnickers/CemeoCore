namespace CeMeOCore.Migrations
{
    using CeMeOCore.DAL.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CeMeOCore.DAL.Context.CeMeoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        //Method that takes care of the dummy data
        protected override void Seed(CeMeOCore.DAL.Context.CeMeoContext context)
        {
            //locations
            Location HoofdkantoorHasselt = new Location
            {
                Name = "Hoofdkantoor Hasselt",
                Street = "Universiteitslaan",
                Number = "9",
                Zip = "3500",
                City = "Hasselt",
                Country = "Belgium",
                State = "",
                Addition = 0
            };

            Location KantoorAntwerpen = new Location
            {
                Name = "Kantoor Antwerpen",
                Street = "Noorderlaan",
                Number = "87",
                Zip = "2030",
                City = "Antwerpen",
                Country = "Belgium",
                State = "",
                Addition = 0
            };

            Location KantoorLeuven = new Location
            {
                Name = "Kantoor Leuven",
                Street = "Interleuvenlaan",
                Number = "16",
                Zip = "3001",
                City = "Leuven",
                Country = "Belgium",
                State = "",
                Addition = 0
            };

            Location KantoorWetteren = new Location
            {
                Name = "Axio Systems",
                Street = "Honderdweg",
                Number = "21",
                Zip = "9230",
                City = "Wetteren",
                Country = "Belgium",
                State = "",
                Addition = 0
            };

            Location KantoorLuik = new Location
            {
                Name = "NSI IT Software & Services – Liège",
                Street = "Chaussée de Bruxelles",
                Number = "174A",
                Zip = "4340",
                City = "Awans",
                Country = "Belgium",
                State = "",
                Addition = 0
            };

            Location KantoorBraine = new Location
            {
                Name = "NSI, Parc de l'Alliance",
                Street = "Avenue de Finlande 8",
                Number = "8",
                Zip = "1420",
                City = "Braine-l'Alleud",
                Country = "Belgium",
                State = "",
                Addition = 0
            };

            Location KantoorNamen = new Location
            {
                Name = "NSI IT Software & Services - Namur ",
                Street = "Business Center - Parc Créalys,  Rue Camille Hubert 5 ",
                Number = "5",
                Zip = "5032",
                City = "Gembloux",
                Country = "Belgium",
                State = "",
                Addition = 0
            };
            Location KantoorVeenendaal = new Location
            {
                Name = "Kantoor Veenendaal",
                Street = "Gildetrom",
                Number = "27-33/35-45",
                Zip = "3905",
                City = "Veenendaal",
                Country = "Netherlands",
                State = "",
                Addition = 0
            };
            /*
            context.Locations.AddOrUpdate(KantoorVeenendaal);
            context.Locations.AddOrUpdate(HoofdkantoorHasselt);
            context.Locations.AddOrUpdate(KantoorAntwerpen);
            context.Locations.AddOrUpdate(KantoorLeuven);
            context.Locations.AddOrUpdate(KantoorWetteren);
            context.Locations.AddOrUpdate(KantoorLuik);
            context.Locations.AddOrUpdate(KantoorBraine);
            context.Locations.AddOrUpdate(KantoorNamen);
            
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
               );*/

            //Rooms
            Room ConferenceRoomFirstFloor = new Room
            {
                Name = "Conference room first floor Hasselt",
                Type = "Conference room for online meetings",
                LocationID = KantoorAntwerpen
            };
            Room meetingRoomFirstFloor = new Room
            {
                Name = "Meetingroom first floor",
                Type = "Normal meetingroom for live meetings",
                LocationID = KantoorBraine
            };
            Room MeetingRoomSecondFloor = new Room
            {
                Name = "Meetingroom second floor",
                Type = "Normal meetingroom for live meetings",
                LocationID = KantoorLuik
            };
            Room MeetingRoomThordFloor = new Room
            {
                Name = "Meetingroom third floor",
                Type = "Normal meetingroom for live meetings",
                LocationID = KantoorLeuven
            };
            Room ConferenceRoomSecondFloor = new Room
            {
                Name = "Conference room second floor",
                Type = "Conference room for online meetings",
                LocationID = KantoorNamen
            };
            Room ConferenceRoomThirdFloor = new Room
            {
                Name = "Conference room third floor",
                Type = "Conference room for online meetings",
                LocationID = KantoorVeenendaal
            };
            Room MeetingRoomBasement = new Room
            {
                Name = "Meetingroom basement",
                Type = "meetingroom for small teammeetings",
                LocationID = KantoorWetteren
            };
            /*
            context.Rooms.AddOrUpdate(
                  l => l.Name,
                  ConferenceRoomFirstFloor,
                  meetingRoomFirstFloor,
                  MeetingRoomSecondFloor,
                  MeetingRoomThordFloor,
                  ConferenceRoomSecondFloor,
                  ConferenceRoomThirdFloor,
                  MeetingRoomBasement
                );*/
        }
        
    }
}
