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
        public string AccountId { get; private set; }

        public decimal Balance { get; set; }
        
        public ClientAccount(string accountId, decimal balance)
        {
            this.AccountId = accountId;
            this.Balance = balance;
        }

        public ClientAccount(string accountId)
        {
            this.AccountId = accountId;
            this.Balance = 0M;
        }

        public void Deposit(decimal value)
        {
            this.Balance += value;
        }

        public void Withdraw(decimal value)
        {
            this.Balance -= value;
        }

        public new string GetHashCode()
            => this.AccountId;

        public static string GetNewAccountId()
        => new Faker().Random.ReplaceNumbers("#####-#");

        public override string ToString()
        {
            return $"Numero Bancario: 777\nNumero da agência: 0001\nNumero da conta: {this.AccountId}\nSaldo:{this.Balance}";
        }
    }
}
