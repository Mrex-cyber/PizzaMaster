﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMaster
{
    public class Location
    {
        public int Id { get; set; }
        public string City { get; set; }
        public List<Employee> Employees { get; set; }
        public double Earnings { get; set; }
        public override string ToString()
        {
            return City;
        }
    }
}
