
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
            //Use Fluent API
            //modelBuilder.Entity<Airplane>().HasKey(a => a.Id);//primary key
            //modelBuilder.Entity<Airplane>().ToTable("Plane");//name table
            // 1 - //modelBuilder.Entity<Airplane>().Property(a => a.Model);
            // 2 //modelBuilder.Entity<Airplane>().Property("Model");
            // 3 - // modelBuilder.Entity<Airplane>().Property(nameof(Airplane.Model));
            modelBuilder.Entity<Airplane>()
                .Property(a => a.Model)
                .HasMaxLength(100)
                .IsRequired();


            modelBuilder.Entity<Client>().ToTable("Passangers");
            modelBuilder.Entity<Client>().Property(c=>c.Name)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("FirstName");
            modelBuilder.Entity<Client>().Property(c => c.Lastname)
                .IsRequired()
                .HasMaxLength(150);
            modelBuilder.Entity<Client>().Property(c => c.Email)
              .IsRequired()
              .HasMaxLength(150);


            modelBuilder.Entity<Flight>().HasKey(f => f.Number);
            modelBuilder.Entity<Flight>().Property(c => c.ArrivalCity)
              .IsRequired()
              .HasMaxLength(100);
            modelBuilder.Entity<Flight>().Property(c => c.DepartureCity)
             .IsRequired()
             .HasMaxLength(100);

            //Relationship navigations
            //many to many *...........*
            modelBuilder.Entity<Flight>()
                .HasMany(f => f.Clients)
                .WithMany(c => c.Flights);
            //one to many 1...........*
            modelBuilder.Entity<Flight>()
                .HasOne(f => f.Airplane)
                .WithMany(a=>a.Flights)
                .HasForeignKey(f=> f.AirplaneId);






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
