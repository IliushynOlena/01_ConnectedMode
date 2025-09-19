
using _05_IntroToEF.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05_IntroToEF
{
    internal class AirportDbContext: DbContext
    {
        //C       R    U       D 
        //Create Read Update Delede
        public AirportDbContext()
        {
           
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-1LCG8OH\SQLEXPRESS;
                                        Initial Catalog = PD_411_Airport;
                                        Integrated Security=True;
                                        Connect Timeout=5;
                                        Encrypt=False;Trust Server Certificate=False;
                                        Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Start initialization
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

        /// Collection 
        ///List<Clients>
        ///Flights
        ///Airplanes
        public DbSet<Client> Clients { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airplane> Airplanes { get; set; }
    }
}
