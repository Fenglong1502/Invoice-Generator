using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceGenerator
{
    public class PurchasedItem
    {

        string productName;
        int qty;
        double unitPrice;

        public PurchasedItem()
        {
        }

        public PurchasedItem(string productName, int qty, double unitPrice)
        {
            this.productName = productName;
            this.qty = qty;
            this.unitPrice = unitPrice;
        }


        public string ProductName { get => productName; set => productName = value; }
        public int Qt { get => qty; set => qty = value; }
        public double UnitPrice { get => unitPrice; set => unitPrice = value; }
    }
}