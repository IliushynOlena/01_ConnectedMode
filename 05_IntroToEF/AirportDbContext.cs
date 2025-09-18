using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
           // this.Database.EnsureDeleted();
            ///this.Database.EnsureCreated();
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
    //Entities (Entity)
    [Table("Passangers")]
    class Client
    {
        //Primary key : Id/id/ID/ Entityname+Id
        public int Id { get; set; }

        [Required, MaxLength(100)]
        [Column("FirstName")]  
        public string Name { get; set; }

        [Required, MaxLength(100)]
        public string Lastname { get; set; }

        [Required, MaxLength(100)]
        //[EmailAddress]
        public string Email { get; set; }
        //null --> not null -------------------
        //not null --> null +++++++++++++++++
        public DateTime Birthdate { get; set; }//not null --> null 
        public string Phone { get; set; }
        /////Relational Type : Flight --- Client (*....*)
        public ICollection<Flight> Flights { get; set; }
       

    }
    class Airplane
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Model { get; set; }

        public int MaxPassangers { get; set; }
        /////Relational Type : Flight --- Airplane (1....*)
        public ICollection<Flight> Flights { get; set; }

    }
    class Flight
    {
        [Key]//set primary key
        public int Number { get; set; }

        [Required]
        [MaxLength(100)]
        public string ArrivalCity { get; set; }

        [Required, MaxLength(100)]
        public string DepartureCity { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }

        /////Relational Type : Flight --- Airplane (1....*)
        public Airplane Airplane { get; set; }//null 0nm454m65h4

        //Foreign key : RelatedEntityName + RelatedEntityNamePrimaryKey
        public int AirplaneId { get; set; }
        /////Relational Type : Flight --- Client (*....*)
        public ICollection<Client> Clients { get; set; }
    }
}
