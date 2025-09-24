using _05_IntroToEF.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace _05_IntroToEF.Helpers
{
    internal static class DbInitializer
    {
        public static void SeedAiplanes(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Airplane>().HasData(new Airplane[]
            {
                new Airplane()
                {
                     Id = 1,
                     Model = "Boeing747",
                     MaxPassangers = 200
                },
                 new Airplane()
                {
                     Id = 2,
                     Model = "Boeing748",
                     MaxPassangers = 200
                },
                    new Airplane()
                {
                     Id = 3,
                     Model = "Broller747",
                     MaxPassangers = 100
                }

         });
        }
        public  static void SeedFlights(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flight>().HasData(new Flight[]
           {
                new Flight()
                {
                     Number = 1,
                     ArrivalCity = "Lviv",
                     DepartureCity = "Kyiv",
                     ArrivalTime = new DateTime(2025,9,21),
                     DepartureTime = new DateTime(2025,9,21),
                     AirplaneId = 1
                },
                 new Flight()
                {
                     Number = 2,
                     ArrivalCity = "Lviv",
                     DepartureCity = "Odessa",
                     ArrivalTime = new DateTime(2025,9,22),
                     DepartureTime = new DateTime(2025,9,22),
                     AirplaneId = 2
                }

           });
        }
    }
}
