
using System.Data.SqlClient;
using StoreApplication.BusinessLogic;
using Microsoft.Extensions.Logging;
using System;


namespace StoreApplication.DataLogic
{
    public class SqlRepository : InterfaceRepo
    {
        // Fields
        private readonly string _connectionString;
        private readonly ILogger<SqlRepository> _logger;

        public SqlRepository(string connectionString, ILogger<SqlRepository> logger) {
            this._connectionString = connectionString; //?? throw new ArgumentNullException(nameof(connectionString));
            this._logger = logger; //?? throw new ArgumentNullException(nameof(logger));
        }

        // Method display all the products in the inventory
        public async Task<IEnumerable<Product>> GetAllProducts() {
            List<Product> productList = new();

            string queryString ="Select ProductID, ProductType, ProductName, Quantity, Cost FROM StoreApp.Product;";

            using (SqlConnection connection = new SqlConnection(this._connectionString))
             
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                await connection.OpenAsync();

                SqlDataReader reader = command.ExecuteReader();

                // Call Read before accessing data.
                while (reader.Read())
                {
                    int ID = reader.GetInt32(0);
                    string ProductType = reader.GetString(1);
                    string ProductName = reader.GetString(2);
                    int Quantity = reader.GetInt32(3);
                    decimal Cost = reader.GetDecimal(4);
                    productList.Add(new(ID, ProductType, ProductName, Quantity, Cost));
                }
                
                // Call Close when done reading.
                //reader.Close();
                await connection.CloseAsync();
            }
            _logger.LogInformation("Executed: GetAllProducts()");
            return productList;
        }


        // This method returns all order histories of a store location
        public async Task<IEnumerable<Orders>> GetAllOrdersLoc(string Location)
        {
            List<Orders> ordersList = new();

            using SqlConnection connection = new SqlConnection(this._connectionString);
            await connection.OpenAsync();

            using SqlCommand cmd = new(
                "Select OrderID, CustomerID, ProductID, Location, Time FROM StoreApp.Orders  WHERE Location = @location ORDER BY Time;", connection);

            cmd.Parameters.AddWithValue("@location", Location);

            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int OrderID = reader.GetInt32(0);
                int CustomerID = reader.GetInt32(1);
                int ProductID = reader.GetInt32(2);
                string Locate = reader.GetString(3);
                DateTime Time = reader.GetDateTime(4);

                ordersList.Add(new(OrderID, CustomerID, ProductID, Locate, Time));
            }

            await connection.CloseAsync();

            _logger.LogInformation("Executed: GetAllOrdersLoc(...)");
            return ordersList;
        }



        public async Task AddCustomer(Customer customer) {
            using SqlConnection connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            int CustomerID = customer.GetCustomerID();
            string FirstName = customer.GetFirstName();
            string LastName = customer.GetLastName();
            string PhoneNumber = customer.GetPhoneNumber();
            //string StreetNumber = customer.GetStreetNumber();
            string Zipcode = customer.GetZipcode();

            string cmdText =
                @"INSERT INTO StoreApp.Customer (CustomerID, FirstName, LastName, PhoneNumber, StreetNumber, Zipcode)
                VALUES
                (@customerID, @firstName, @lastName, @phoneNumber, @streetNumber, @zipcode);";

            using SqlCommand cmd = new SqlCommand(cmdText, connection);

            cmd.Parameters.AddWithValue("@customerID", CustomerID);
            cmd.Parameters.AddWithValue("@firstName", FirstName);
            cmd.Parameters.AddWithValue("@lastName", LastName);
            cmd.Parameters.AddWithValue("@phoneNumber", PhoneNumber);
            //cmd.Parameters.AddWithValue("@streetNumber", StreetNumber);
            cmd.Parameters.AddWithValue("@zipcode", Zipcode);

            cmd.ExecuteNonQuery();

            await connection.CloseAsync();
        }

        public async Task<Customer> GetCustomer(string FirstName, string LastName)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            string cmdText = @"SELECT CustomerID, FirstName, LastName, PhoneNumber, Zipcode FROM StoreApp.Customer WHERE FirstName = @firstName AND LastName = @lastName;";

            using SqlCommand cmd = new SqlCommand(cmdText, connection);

            cmd.Parameters.AddWithValue("@firstName", FirstName);
            cmd.Parameters.AddWithValue("@lastName", LastName);

            using SqlDataReader reader = cmd.ExecuteReader();

            Customer tmpCustomer;
            while (reader.Read())
            {
                return tmpCustomer = new Customer(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                                        reader.GetString(3), reader.GetString(4));
            }
            await connection.CloseAsync();
            _logger.LogInformation("Executed: GetCustomer(...)");
            Customer noCustomer = new();
            return noCustomer;
        }

        public async Task<Product> GetOrderDetails(int CustomerID)
        {
            //throw new NotImplementedException();
            using SqlConnection connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            string cmdText = @"SELECT ProductID, ProductType, ProductName, Quantity, Cost FROM StoreApp.Product WHERE ProductID = (SELECT ProductID FROM StoreAPP.Orders WHERE CustomerID = @customerID);";

            using SqlCommand cmd = new SqlCommand(cmdText, connection);

            cmd.Parameters.AddWithValue("@customerID", CustomerID);

            using SqlDataReader reader = cmd.ExecuteReader();

            Product tmpProduct;
            while (reader.Read())
            {
                return tmpProduct = new Product(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                                        reader.GetInt32(3), reader.GetDecimal(4));
            }
            await connection.CloseAsync();
            _logger.LogInformation("Executed: GetOrderDetails(...)");
            Product noProduct = new();
            return noProduct;
        }


        // This methods display all order history by a customer ID
        public async Task<IEnumerable<Orders>> GetAllOrdersCust(int CustomerID)
        {
            List<Orders> ordersList = new();

            using SqlConnection connection = new SqlConnection(this._connectionString);
            await connection.OpenAsync();

            using SqlCommand cmd = new(
                "Select OrderID, CustomerID, ProductID, Location, Time FROM StoreApp.Orders WHERE CustomerID = @customerID ORDER BY Time;", connection);

            cmd.Parameters.AddWithValue("@customerID", CustomerID);

            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int OrderID = reader.GetInt32(0);
                int ID = reader.GetInt32(1);
                int ProductID = reader.GetInt32(2);
                string Location = reader.GetString(3);
                DateTime Time = reader.GetDateTime(4);

                ordersList.Add(new(OrderID, ID, ProductID, Location, Time));
            }

            await connection.CloseAsync();
            _logger.LogInformation("Executed: GetAllOrdersCust(...)");
            return ordersList;
        }

        public async Task AddOrders(Orders orders)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            int OrderID = orders.GetOrderID();
            int CustomerID = orders.GetCustomerID();
            int ProductID = orders.GetProductID();
            string Location = orders.GetLocation();
            //string StreetNumber = customer.GetStreetNumber();
            DateTime Time = orders.GetTime();

            string cmdText =
                @"INSERT INTO StoreApp.Customer (OrderID, CustomerID, ProductID, Location, Time)
                VALUES
                (@orderID, @customerID, @productID, @location, @time);";

            using SqlCommand cmd = new SqlCommand(cmdText, connection);

            cmd.Parameters.AddWithValue("@orderID", OrderID);
            cmd.Parameters.AddWithValue("@customerID", CustomerID);
            cmd.Parameters.AddWithValue("@productID", ProductID);
            cmd.Parameters.AddWithValue("@location", Location);
            //cmd.Parameters.AddWithValue("@streetNumber", StreetNumber);
            cmd.Parameters.AddWithValue("@time", Time);

            cmd.ExecuteNonQuery();

            await connection.CloseAsync();
        }

        // display all orders
        public async Task<IEnumerable<Orders>> GetAllOrders() {

            List<Orders> orderList = new();

            string queryString = "Select OrderID, CustomerID, ProductName, Product, Location FROM StoreApp.Orders;";

            using (SqlConnection connection = new SqlConnection(this._connectionString))

            {
                SqlCommand command = new SqlCommand(queryString, connection);
                await connection.OpenAsync();

                SqlDataReader reader = command.ExecuteReader();

                // Call Read before accessing data.
                while (reader.Read())
                {
                    int OrderID = reader.GetInt32(0);
                    int CustomerID = reader.GetInt32(1);
                    int ProductID = reader.GetInt32(2);
                    string Location = reader.GetString(3);
                    DateTime Time = reader.GetDateTime(4);
                    orderList.Add(new(OrderID, CustomerID, ProductID, Location, Time));
                }

                // Call Close when done reading.
                //reader.Close();
                await connection.CloseAsync();
            }
            _logger.LogInformation("Executed: GetAllProducts()");
            return orderList;
        }
    }
}