using CinemaPink.Data;
using CinemaPink.Models;
using System;
using System.Linq;

namespace CinemaPink.Data
{
    public static class DbInitializer
    {
        //public static void Initialize(Cinema_context context)
        //{
        //    context.Database.EnsureCreated();

        //    // Look for any students.
        //    if (context.Films.Any())
        //    {
        //        return;   // DB has been seeded
        //    }

        //    var films = new Film[]
        //    {
        //    new Film{Title="Star Wars VIII."},
        //    new Film{Title="Harry Potter and the Prisoner of Azkaban"},
        //    new Film{Title="Black Panther"},
        //    new Film{Title="Avengers 3"},

        //    };
        //    foreach (Film f in films)
        //    {
        //        context.Films.Add(f);
        //    }
        //    context.SaveChanges();

        //    var rooms = new Room[]
        //    {
        //    new Room{Name="First"},
        //    new Room{Name="Second"},
        //    new Room{Name="Third"},
        //    new Room{Name="Fourth"},
        //    };
        //    foreach (Room r in rooms)
        //    {
        //        context.Rooms.Add(r);
        //    }
        //    context.SaveChanges();

        //    var seats = new Seat[]
        //    {
        //    new Seat{RoomID=1},
        //    new Seat{RoomID=1},
        //    new Seat{RoomID=2},
        //    new Seat{RoomID=1},
        //    new Seat{RoomID=3},
        //    new Seat{RoomID=1}
        //    };
        //    foreach (Seat s in seats)
        //    {
        //        context.Seats.Add(s);
        //    }
        //    context.SaveChanges();

        //    var projections = new Projection[]
        //        {
        //            new Projection{FilmID=1,RoomID=1},
                    
        //        };
        //}
    }
}