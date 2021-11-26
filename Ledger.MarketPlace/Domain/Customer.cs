using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ledger.MarketPlace.Domain
{
    public class Customer
    {
        public Customer(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
        public Loan Loan { get; set; }
    }
}
