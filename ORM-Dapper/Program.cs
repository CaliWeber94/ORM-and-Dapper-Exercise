using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace ORM_Dapper
{
    public class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");

            IDbConnection conn = new MySqlConnection(connString);

            var departmentRepo = new DapperDepartmentRepository(conn);
            var departments = departmentRepo.GetAllDepartments();
            departmentRepo.InsertDepartment("New Department");

            foreach (var department in departments)
            {
                Console.WriteLine(department.DepartmentID);
                Console.WriteLine(department.Name);
            }

            var productRepo = new DapperProductRepository(conn);
            var products = productRepo.GetAllProducts();
            productRepo.CreateProduct("New Product", 100.00, 5);
            foreach(var product in products)
            {
                Console.WriteLine(product.ProductID);
                Console.WriteLine(product.Name);
                Console.WriteLine(product.Price);
                Console.WriteLine(product.StockLevel);
                Console.WriteLine(product.OnSale);
                Console.WriteLine(product.CategoryID);

            }
        }
    }
}
