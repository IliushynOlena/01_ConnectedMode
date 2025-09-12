using System.Xml.Linq;
using System.Xml;
using _03_data_access.Models;

namespace _02_CRUD_Interface
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source = DESKTOP-1LCG8OH\SQLEXPRESS; 
                                        Initial Catalog = SportShop;
                                        Integrated Security = true; 
                                        TrustServerCertificate=True;";

           
           
            Product product = new Product()
            {
                 Name = "Oleg",
                 Type = "Equipment",
                 Quantity = 15,
                 Cost = 500,
                 Producer = "Італія",
                 Price = 300
            };
           // product.Cost = 5000;
            using (SportShopDb shopDb = new SportShopDb(connectionString))
            {
                shopDb.Create_As_Insert(product);

                var products = shopDb.Read_Get_All();
                foreach (var p in products)
                {
                    Console.WriteLine(p.ToString());
                }

                Product p1 = shopDb.GetOne(59);
                Console.WriteLine(p1.Name + "   " + p1.Cost);
                p1.Cost -= 15000;
                Console.WriteLine(p1.Name + "   " + p1.Cost);

                shopDb.Update(p1);
                //shopDb.Delete(58);
                products = shopDb.Read_Get_All();
                foreach (var p in products)
                {
                    Console.WriteLine(p.ToString());
                }
                Console.WriteLine(  "----------------------------------");
                Console.WriteLine("Enter name of product");
                string name = Console.ReadLine();
                products = shopDb.Get_By_Name(name);
                foreach (var p in products)
                {
                    Console.WriteLine(p.ToString());
                }




            }//shopDb.Dispose()





        }
    }
}
