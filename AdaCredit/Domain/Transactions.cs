using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit.Domain
{
    public partial class Transactions
    {
        public string OriginbankId { get; set; }
        public string OriginAgencyId { get; set; }
        public string OriginAccountId { get; set; }
        public string DestinybankId { get; set; }
        public string DestinyAgencyId { get; set; }
        public string DestinyAccountId { get; set; }
        public string TransctionType { get; set; }
        public decimal TransactionAmount { get; set; }

        public Transactions(string originbankId, string originAgencyId, string originAccountId, string destinybankId, string destinyAgencyId, string destinyAccountId, string transctionType, decimal transactionAmount)
        {
            this.OriginbankId = originbankId;
            this.OriginAgencyId = originAgencyId;
            this.OriginAccountId = originAccountId;
            this.DestinybankId = destinybankId;
            this.DestinyAgencyId = destinyAgencyId;
            this.DestinyAccountId = destinyAccountId;
            this.TransctionType = transctionType;
            this.TransactionAmount = transactionAmount;
        }
        public decimal CalculateTax(DateTime TransactionDate)
        {
            if (DateTime.Compare(TransactionDate, new DateTime(2022, 12, 1)) < 0)
            {
                return this.TransactionAmount;
            }
            if (this.TransctionType == "TED")
            {
                return this.TransactionAmount + 5;
            }
            else if(this.TransctionType == "DOC")
            {
                return this.TransactionAmount <= 500 ? 1 + this.TransactionAmount*(1.01M) : this.TransactionAmount + 6;
            }
            return this.TransactionAmount;
        }
    }
}
