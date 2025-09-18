using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_LinqToSQL
{
   
    internal class Program
    {
        static void Main(string[] args)
        {
            SportShopDbContextDataContext db = new SportShopDbContextDataContext();
           
            //CRUD Interface
            var products = db.Products;
            foreach (var p in products)
            {
                Console.WriteLine($"Product: {p.Id,5}. {p.Name,-15} . {p.CostPrice}$ ");
            }
            Console.WriteLine("----------------------------");
            //products more > 1000
            //---------------------3---------------
            var mostValue = db.Products.Where(p => p.CostPrice > 1000)
                .OrderByDescending(p => p.CostPrice).Take(5);

            //----------------1-----------------
            var mostValue1 = db.Products.Where(FilteredPredicate)
               .OrderByDescending(p => p.CostPrice).Take(5);

            //----------------------2----------------
            var mostValue2 = db.Products.Where(delegate (Product p) { return p.CostPrice > 1000; })
              .OrderByDescending(p => p.CostPrice).Take(5);

            //var mostValue = (from p in db.Products
            //                where p.CostPrice > 1000
            //                orderby p.Price descending
            //                select p).Take(5);


            foreach (var p in mostValue)
            {
                Console.WriteLine($"Product: {p.Id,5}. {p.Name,-15} . {p.CostPrice}$ ");
            }


            Product product = new Product()
            {
                Name = "Football ball",
                TypeProduct = "Equipment",
                Quantity = 15,
                CostPrice = 500,
                Producer = "Італія",
                Price = 300
            };

            db.Products.InsertOnSubmit(product);
            db.SubmitChanges();



            Product pr = db.Products.Where(p => p.Id == 3).FirstOrDefault();
            Console.WriteLine($"Product with id {pr.Id}. {pr.Name}. Price = {pr.CostPrice}");
            pr.CostPrice -= 16000;
            Console.WriteLine($"Product with id {pr.Id}. {pr.Name}. Price = {pr.CostPrice}");
            db.SubmitChanges();
        }
        static bool FilteredPredicate(Product p)
        {
            return p.CostPrice > 1000;
        }
    }
}
