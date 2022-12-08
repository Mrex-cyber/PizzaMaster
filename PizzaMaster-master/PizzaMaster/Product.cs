using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMaster
{
    public class Product
    {
        private double earning;
        public int Id { get; set; }
        public string Name { get; set; }  
        public double SellingPrice { get; set; }
        public double BuyingPrice { get; set; }
        public int Amount { get; set; }
        public double Earnings { 
            get { return earning; }
            set { earning = (SellingPrice - BuyingPrice) * Amount; } 
        }
        public int LocationId { get; set; }
        public Location Location { get; set; }
    }
}
