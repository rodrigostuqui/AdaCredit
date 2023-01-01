using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit.Domain
{
    public class Client : IPerson
    {
        public string Name { get; set; }
        public string Id { get ; set ; }
        public bool Status { get ; set ; }
        public ClientAccount Account { get; set; }

        public Client(string name, string id, bool status, ClientAccount account)
        {
            this.Name = name;
            this.Id = id;
            this.Status = status;
            this.Account = account;
        }

        public Client(string name, string id, ClientAccount account)
        {
            this.Name = name;
            this.Id = id;
            this.Status = true;
            this.Account = account;
        }

        public new string GetHashCode()
        => this.Id;
    }
}
