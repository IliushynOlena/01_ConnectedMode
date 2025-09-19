using System;
using _05_IntroToEF.Entities;

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
            context.SaveChanges();

            foreach (var client in context.Clients)
            {
                Console.WriteLine(client.Name + " " + client.Phone);
            }

            Client cl = context.Clients.Find(1);
 
            Console.WriteLine($"{cl.Name}  {cl.Email} {cl.Birthdate.ToString("yyyy-MM-dd")} ");

            if(cl != null)
            {
                context.Clients.Remove(cl);
                context.SaveChanges();  
            }
        }
    }
}
