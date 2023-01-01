using AdaCredit.Data;
using AdaCredit.Domain;
using AdaCredit.UI.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit.Services
{
    public class ClientServices
    {
        static Repository<Client> clientRepository = new Repository<Client>("client.txt");

        public static Client findClientbyAccountId(string accountId)
        {
            clientRepository.loadData();
            return clientRepository._data.FirstOrDefault(x => x.Account.AccountId == accountId);
        }

        public static string findAccountIdById(string clientId)
        {
            clientRepository.loadData();
            var user = findClientById(clientId);
            return user.Account.AccountId;
        }

        public static int ChangeEmployeeStatus(string employeeId, bool status)
        {
            clientRepository.loadData();
            var user = findClientById(employeeId);
            if (user != null)
            {
                user.Status = status;
                clientRepository.UpdateData();
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public static Client findClientById(string clientId)
        {
            clientRepository.loadData();
            var user = clientRepository._data.FirstOrDefault(x => x.GetHashCode() == clientId);
            return user;
        }
        public static int CreateClient(string name, string id, string pass)
        {
            Client user;
            string accountId;
            if (findClientById(id) != null)
            {
                return 1;
            }
            clientRepository.loadData();
            do
            {
                accountId = ClientAccount.GetNewAccountId();
                user = findClientbyAccountId(accountId);
            } while (user != null);
            Client client = new Client(name, id, new ClientAccount(pass, accountId));
            clientRepository.loadData();
            clientRepository.saveData(client);
            return 0;
        }

    }
}
