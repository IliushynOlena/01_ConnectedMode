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
            //string cmdText = $@"INSERT INTO Products
            //                  VALUES ('{product.Name}', 
            //                          '{product.Type}',
            //                           {product.Quantity}, 
            //                           {product.Cost}, 
            //                          '{product.Producer}', 
            //                           {product.Price})";
            string cmdText = $@"INSERT INTO Products
                              VALUES (@name,@type,@quantity,@cost,@producer,@price)";

            SqlCommand command = new SqlCommand(cmdText, sqlConnection);
            command.Parameters.AddWithValue("name", product.Name);
            command.Parameters.AddWithValue("type", product.Type);
            command.Parameters.AddWithValue("quantity", product.Quantity);
            command.Parameters.AddWithValue("cost", product.Cost);
            command.Parameters.AddWithValue("producer", product.Producer);
            command.Parameters.AddWithValue("price", product.Price);
            command.CommandTimeout = 5; // default - 30sec
           
            int rows = command.ExecuteNonQuery();
            Console.WriteLine(rows + " rows affected!");
        }
        private List<Product> GetProductsByQuery(SqlDataReader reader)
        {         
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
        public List<Product> Read_Get_All() 
        {
            string cmdText = $@"select * from Products";
            SqlCommand command = new SqlCommand(cmdText, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();
            return GetProductsByQuery(reader);    
        }
        public List<Product> Get_By_Name(string _user_name)
        {
            string cmdText = $@"select * from Products where Name = @name";
            SqlCommand command = new SqlCommand(cmdText, sqlConnection);
            //command.Parameters.Add("name", System.Data.SqlDbType.NVarChar).Value = _user_name;
            SqlParameter parameter = new SqlParameter
            {
                ParameterName = "name",
                SqlDbType = System.Data.SqlDbType.NVarChar,
                Value = _user_name
            };
            command.Parameters.Add(parameter);  

            SqlDataReader reader = command.ExecuteReader();
            return GetProductsByQuery(reader);
        }
        public Product GetOne(int id)
        {
            string cmdText = $@"select * from Products where Id = {id}";
            SqlCommand command = new SqlCommand(cmdText, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();
            return GetProductsByQuery(reader).FirstOrDefault()!;
        }
        public void Update(Product product) 
        {
            string cmdText = $@"UPDATE Products
                              SET Name =@name, 
                                  TypeProduct =@type, 
                                  Quantity =@quantity, 
                                  CostPrice =@cost, 
                                  Producer =@producer, 
                                  Price =@price
                                  where Id = {product.Id}";

            SqlCommand command = new SqlCommand(cmdText, sqlConnection);
            command.Parameters.AddWithValue("name", product.Name);
            command.Parameters.AddWithValue("type", product.Type);
            command.Parameters.AddWithValue("quantity", product.Quantity);
            command.Parameters.AddWithValue("cost", product.Cost);
            command.Parameters.AddWithValue("producer", product.Producer);
            command.Parameters.AddWithValue("price", product.Price);
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
