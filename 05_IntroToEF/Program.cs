using System;
using System.Linq;
using _05_IntroToEF.Entities;
using Microsoft.EntityFrameworkCore;

namespace _05_IntroToEF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AirportDbContext context = new AirportDbContext();

            context.Clients.Add(new Client() { 
                 Name = "Oleg",
                 Lastname = "Petryk",
                 Email = "oleg@gmail.com",
                 Birthdate = new DateTime(1995,12,5),
                 Phone = "+380975884525"
            });
            ///context.SaveChanges();

            foreach (var client in context.Clients)
            {
                Console.WriteLine(client.Name + " " + client.Phone);
            }

            Client cl = context.Clients.Find(3);
 
            Console.WriteLine($"{cl?.Name}  {cl?.Email} {cl?.Birthdate.ToString("yyyy-MM-dd")}\n" +
                $"{cl?.Flights?.Count} ");
            //Explicit loading data : Context.Entry(entity).Collection/References.Load()
            //
            context.Entry(cl).Collection(cl => cl.Flights).Load();
            Console.WriteLine($"{cl?.Name}  {cl?.Email} {cl?.Birthdate.ToString("yyyy-MM-dd")}\n" +
              $"{cl.Flights.Count} ");
            Console.WriteLine("--------------- All flights ------------");
            foreach (var f in cl.Flights)
            {
                Console.WriteLine(f.ArrivalCity + " " + f.DepartureCity);

            }
            Console.WriteLine("------------------------------------");

            //if(cl != null)
            //{
            //    context.Clients.Remove(cl);
            //    context.SaveChanges();  
            //}
            //JOIN : loading data Include(relation data)
            var flight = context.Flights
                .Include(f => f.Airplane)
                .Include(f=>f.Clients)
                //JOIN Airplane
                .Where(f => f.ArrivalCity == "Lviv")
                .OrderBy(f=> f.DepartureTime);
            foreach (var f in flight)
            {
                Console.WriteLine($"Arrival city : {f.ArrivalCity}. " +
                    $"Departure city : {f.DepartureCity}\n " +
                    $"Departure time : {f.DepartureTime.ToShortDateString()}\n" +
                    $"Airplane : {f.AirplaneId} . {f.Airplane.Model}\n" +
                    $"Max count passangers : {f.Airplane.MaxPassangers}");
            }
        }
    }
}
