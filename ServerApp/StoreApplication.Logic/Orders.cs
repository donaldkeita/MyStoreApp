using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections;
using System.Collections.Generic;

namespace StoreApplication.BusinessLogic
{
    public class Orders
    {
        // Fields
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int ProductID { get; set; }
        public string Location { get; set; }
        public DateTime Time { get; set; }
      

        public Orders()
        {
            this.OrderID = 0;
            this.CustomerID = 0;    
            this.ProductID = 0;
            this.Location = "";
            this.Time = DateTime.Now;
        }

        public Orders(int OrderID, int customerID, int productID, string location, DateTime time)
        {
            this.OrderID = OrderID;
            this.CustomerID = customerID;
            this.ProductID = productID;
            this.Location = location;
            this.Time = time;
        }

        public int GetOrderID() { return OrderID; }
        public int GetCustomerID() { return CustomerID; }
        public int GetProductID() { return ProductID; }
        public string GetLocation() { return Location; }
        public DateTime GetTime() { return Time; }
    }
}