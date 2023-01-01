using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;


namespace AdaCredit.Domain
{
    public sealed class Employee : IPerson
    {
        public string Name { get; private set; }
        public string Id { get; private set; }
        public string Login { get; private set; }
        public string Password { get; private set; }
        public DateTime LastVisit { get; private set; }
        public bool Status { get; set; } = true;

        public Employee(string name, string id, string login, string password, DateTime lastVisit,bool status)
        {
            this.Name = name;
            this.Id = id;
            this.Login = login;
            this.Password = password;
            this.LastVisit = lastVisit;
            this.Status = status;
        }

        public Employee(string name, string id, string login, string password)
        {
            this.Name = name;
            this.Id = id;
            this.LastVisit = DateTime.Now;
            this.Login = login;
            this.Password = BC.HashPassword(password, BC.GenerateSalt());
        }

        public void UpdateLastVisit()
        {
            this.LastVisit = DateTime.Now;
        }

        public void UpdatePassword(string newPass)
        {
            this.Password = BC.HashPassword(newPass, BC.GenerateSalt());
        }
        public new string GetHashCode()
        {
            return this.Id;
        }
    }
}
