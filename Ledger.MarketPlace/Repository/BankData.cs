using Ledger.MarketPlace.Domain;
using System;
using System.Collections.Generic;

namespace Ledger.MarketPlace.Repository
{
    public sealed class BankData
    {
        public List<Bank> Banks { get; set; }
        BankData()
        {
            Banks = new List<Bank>();
        }
        private static readonly Lazy<BankData> lazy
          = new Lazy<BankData>(() => new BankData());

        public static BankData Instance
            => lazy.Value;
    }
}