using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace AdaCredit.Domain
{
    public class ClientAccount
    {
        public string Password { get; private set; }
        public string BankId { get; set; } = "777";
        public string AccountId { get; private set; }

        public string AgencyId { get; set; } = "0001";
        
        public ClientAccount(string password, string bankId, string accountId, string agencyId)
        {
            this.Password = password;
            this.BankId = bankId;
            this.AccountId = accountId;
            this.AgencyId = agencyId;
        }

        public ClientAccount(string password, string accountId)
        {
            this.Password = BC.HashPassword(password, BC.GenerateSalt());
            this.AgencyId = "0001";
            this.BankId = "777";
            this.AccountId = accountId;
        }

        public static string GetNewAccountId()
        => new Faker().Random.ReplaceNumbers("#####-#");
    }
}
