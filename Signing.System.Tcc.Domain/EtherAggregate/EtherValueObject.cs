using System;
using System.Collections.Generic;
using System.Text;

namespace Signing.System.Tcc.Domain.EtherAggregate
{
    public class EtherValueObject
    {
        public EtherValueObject(decimal totalAmount, string baseCurrency, string targetCurrency)
        {
            Amount = totalAmount;

            BaseCurrency = baseCurrency;

            TargetCurrency = targetCurrency;
        }

        public decimal Amount { get; }

        public string BaseCurrency { get; }

        public string TargetCurrency { get; }
    }
}
