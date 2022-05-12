using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApplication.BusinessLogic
{
    public class Store
    {
        // Fields
        public int ID { get; set; } 
        public string Address { get; set; }
        // inventory = productID
        public int InventoryID { get; set; }

        // Parametized constructor
        public Store(int ID, string address, int inventory)
        {
            this.ID = ID;
            this.Address = address;
            this.InventoryID = inventory;
        }
    }
}
