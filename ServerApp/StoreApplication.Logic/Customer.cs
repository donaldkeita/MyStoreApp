using System;
using System.Collections;
using System.Collections.Generic;

namespace StoreApplication.BusinessLogic
{
    public class Customer
    {
        // Fields
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }    
        public string PhoneNumber { get; set; }
        //private string StreetNumber;
        public string Zipcode { get; set; }

        public Customer() {
            this.CustomerID = 0;
            this.FirstName = "";
            this.LastName = "";
            this.PhoneNumber = "";
            //this.StreetNumber = "";
            this.Zipcode = "";
        }

        public Customer(int customerID, string firstName, string lastName, string phoneNumber, string zipcode)
        {
            this.CustomerID = customerID;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.PhoneNumber = phoneNumber;
            //this.StreetNumber = streetNumber;
            this.Zipcode = zipcode;
        }
        public int GetCustomerID() { 
            return this.CustomerID; }
        public string GetFirstName() { return this.FirstName; }

        public string GetLastName() { return this.LastName; }

        public string GetPhoneNumber() { return this.PhoneNumber; }

        //public string GetStreetNumber() { return this.StreetNumber; }

        public string GetZipcode() { return this.Zipcode; }



    }
}