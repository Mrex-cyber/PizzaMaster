using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMaster
{
    public class Location
    {
        private double earnings;
        public int Id { get; set; }
        public string City { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Product> Products { get; set; }
        public double Earnings {
            get { return earnings; }
            set { earnings = CalculateEarnings(Products); }
        }

        private double CalculateEarnings(List<Product> products)
        {
            double sum = 0;

            for (int i = 0; i < products.Count; i++)
            {
                sum += products[i].Earnings;
            }
            return sum;
        }
        public override string ToString()
        {
            return City;
        }
    }
}
