using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ledger.MarketPlace.Domain
{
    public class Bank
    {
        public Bank(string name)
        {
            Name = name;
            this.Customers = new List<Customer>();
        }
        public string Name { get; set; }
        public List<Customer> Customers { get; set; }
    }
}
