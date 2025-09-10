using System.Data.SqlClient;
using System.Text;
using System.Xml.Linq;
using System.Xml;

namespace _02_CRUD_Interface
{
    class SportShopDb: IDisposable
    {
        private SqlConnection sqlConnection;
      
        public SportShopDb(string connectionString)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
        }
        //C       R    U       D 
        //Create Read Update Delede
        public void Create_As_Insert(Product product) 
        {
            string cmdText = $@"INSERT INTO Products
                              VALUES ('{product.Name}', 
                                      '{product.Type}',
                                       {product.Quantity}, 
                                       {product.Cost}, 
                                      '{product.Producer}', 
                                       {product.Price})";

            SqlCommand command = new SqlCommand(cmdText, sqlConnection);
            command.CommandTimeout = 5; // default - 30sec
           
            int rows = command.ExecuteNonQuery();
            Console.WriteLine(rows + " rows affected!");
        }
        public List<Product> Read_Get_All() 
        {
            string cmdText = $@"select * from Products";

            SqlCommand command = new SqlCommand(cmdText, sqlConnection);
             SqlDataReader reader = command.ExecuteReader();
            Console.OutputEncoding = Encoding.UTF8;

            List<Product> products = new List<Product>();   
         
            while (reader.Read())
            {
                products.Add(
                    new Product()
                    {
                        Id = (int)reader[0],
                        Name = (string)reader[1],
                        Type = (string)reader[2],
                        Quantity = (int)reader[3],
                        Cost = (int)reader[4],
                        Producer = (string)reader[5],
                        Price = (int)reader[6]
                    });

             
            }

            reader.Close();
            return products;    
        }
        public Product GetOne(int id)
        {
            string cmdText = $@"select * from Products where Id = {id}";

            SqlCommand command = new SqlCommand(cmdText, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();
            Console.OutputEncoding = Encoding.UTF8;

            Product product = new Product();
            while (reader.Read())
            {
                product.Id = (int)reader[0];
                product.Name = (string)reader[1];
                product.Type = (string)reader[2];
                product.Quantity = (int)reader[3];
                product.Cost = (int)reader[4];
                product.Producer = (string)reader[5];
                product.Price = (int)reader[6];                  

            }

            reader.Close();
            return product;
        }
        public void Update(Product product) 
        {
            string cmdText = $@"UPDATE Products
                              SET Name ='{product.Name}', 
                                  TypeProduct ='{product.Type}', 
                                  Quantity ={product.Quantity}, 
                                  CostPrice ={product.Cost}, 
                                  Producer ='{product.Producer}', 
                                  Price ={product.Price}
                                  where Id = {product.Id}";

            SqlCommand command = new SqlCommand(cmdText, sqlConnection);
            command.CommandTimeout = 5; // default - 30sec

            command.ExecuteNonQuery();

        }
        public void Delete(int id) 
        {
            string cmdText = $@"delete Products where Id = {id}";

            SqlCommand command = new SqlCommand(cmdText, sqlConnection);       
            command.ExecuteNonQuery();
        }

        public void Dispose()
        {
            sqlConnection.Close();  
        }


    }
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
                 Name = "Кроси",
                 Type = "Взуття",
                 Quantity = 4,
                 Cost = 40000,
                 Producer = "Італія",
                 Price = 30000
            };
           // product.Cost = 5000;
            using (SportShopDb shopDb = new SportShopDb(connectionString))
            {
                //shopDb.Create_As_Insert(product);

                var products = shopDb.Read_Get_All();
                foreach (var p in products)
                {
                    Console.WriteLine(p.ToString());
                }

                Product p1 = shopDb.GetOne(56);
                Console.WriteLine(p1.Name + "   " + p1.Cost);
                p1.Cost -= 15000;
                Console.WriteLine(p1.Name + "   " + p1.Cost);

                shopDb.Update(p1);

                products = shopDb.Read_Get_All();
                foreach (var p in products)
                {
                    Console.WriteLine(p.ToString());
                }




            }//shopDb.Dispose()





        }
    }
}
