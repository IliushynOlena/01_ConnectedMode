using Microsoft.Data.SqlClient;
using System.Text;

namespace _01_ConnectedMode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //connectionString : Property = value;property2 = value;
            /* SQL Server:
              - Windows Authentication:    "Data Source=server_name;
                                            Initial Catalog=db_name;
                                            Integrated Security=True";
              - SQL Server Authentication: "Data Source=server_name;
                                            Initial Catalog=db_name;
                                            User ID=login;Password=password";
          */
            //connectionString:  Property=value;Property2=value2;
            string connectionString = @"Data Source = DESKTOP-1LCG8OH\SQLEXPRESS; 
                                        Initial Catalog = SportShop;
                                        Integrated Security = true; TrustServerCertificate=True;
";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            Console.WriteLine("Connected success!");


            #region Execute Non-Query
            //string cmdText = @"INSERT INTO Products
            //                  VALUES ('Gloves', 'Equipment', 50, 500, 'Ukraine', 50)";

            //SqlCommand command = new SqlCommand(cmdText, sqlConnection);
            //command.CommandTimeout = 5; // default - 30sec

            //////// ExecuteNonQuery - виконує команду яка не повертає результат 
            ///////(insert, update, delete...),
            ////////але метод повертає кількітсь рядків, які були задіяні
            //int rows = command.ExecuteNonQuery();

            //Console.WriteLine(rows + " rows affected!");
            #endregion
            #region Execute Scalar
            //string cmdText = @"select AVG(Price) from Products";

            //SqlCommand command = new SqlCommand(cmdText, sqlConnection);

            //// Execute Scalar - виконує команду, яка повертає одне значення
            //int res = (int)command.ExecuteScalar();

            //Console.WriteLine("Result: " + res);
            #endregion
            int id = 2;
            string name = "Pукавиці";
            #region Execute Reader
            string cmdText = $@"select * from Products
                              where Id = {id}";

            string cmdText2 = $@"select * from Products
                              where Name = '{name}'";

            SqlCommand command = new SqlCommand(cmdText, sqlConnection);

            //// ExecuteReader - виконує команду select та повертає результат у вигляді
            //// DbDataReader
            SqlDataReader reader = command.ExecuteReader();

            Console.OutputEncoding = Encoding.UTF8;

            //// відображається назви всіх колонок таблиці
            for (int i = 0; i < reader.FieldCount; i++)
            {
                Console.Write($" {reader.GetName(i),14}");
            }
            Console.WriteLine("\n---------------------------------------------------------------------------------------------------------------------");

            //////// відображаємо всі значення кожного рядка
            while (reader.Read())
            {
              
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write($" {reader[i],14} ");
                }
                Console.WriteLine();
            }

            reader.Close();
            #endregion

            sqlConnection.Close();
        }
    }
}
