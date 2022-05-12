using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApplication.BusinessLogic;

namespace StoreApplication.DataLogic
{
    public interface InterfaceRepo
    {
        Task AddOrders(Orders orders);
        Task AddCustomer(Customer customer);
        Task<Customer> GetCustomer(string fname, string lname);
        Task<Product> GetOrderDetails(int CustomerID);
        Task<IEnumerable<Orders>> GetAllOrdersLoc(string Location);
        Task<IEnumerable<Orders>> GetAllOrdersCust(int CustomerID);
        Task<IEnumerable<Product>> GetAllProducts();
        Task<IEnumerable<Orders>> GetAllOrders();
    }
}
