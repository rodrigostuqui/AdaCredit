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
        public static Repository<Client> clientRepository = new Repository<Client>("client.csv");

        public static Client findClientbyAccountId(string accountId)
        {
            clientRepository.loadData();
            return clientRepository._data.FirstOrDefault(x => x.Account.AccountId == accountId);
        }

        public static string UpdateClientInfo(string clientId, string name)
        {
            clientRepository.loadData();
            var user = findClientById(clientId);
            if (user != null)
            {
                user.Name = name;
                clientRepository.UpdateData();
                return "0";
            }
            return "1";
        }


        public static string findAccountIdById(string clientId)
        {
            clientRepository.loadData();
            var user = findClientById(clientId);
            return user.Account.AccountId;
        }

        public static string getAllInfo(string clientId) 
        {
            clientRepository.loadData();
            var user = findClientById(clientId);

            return user != null ? user.ToString() : "1";
        }

        public static List<Client> getAllActiveClients()
        {
            clientRepository.loadData();
            return clientRepository._data.Where(x => x.Status == true).ToList();
        }

        public static List<Client> getAllInactiveClients()
        {
            clientRepository.loadData();
            return clientRepository._data.Where(x => x.Status == false).ToList();
        }

        public static string ChangeEmployeeStatus(string clientId, bool status)
        {
            clientRepository.loadData();
            var user = findClientById(clientId);
            if (user != null)
            {
                user.Status = status;
                clientRepository.UpdateData();
                return "0";
            }
            else
            {
                return "1";
            }
        }

        public static Client findClientById(string clientId)
        {
            clientRepository.loadData();
            var user = clientRepository._data.FirstOrDefault(x => x.GetHashCode() == clientId);
            return user;
        }
        public static string CreateClient(string name, string id)
        {
            Client user;
            string accountId;
            if (findClientById(id) != null)
            {
                return "1";
            }
            clientRepository.loadData();
            do
            {
                accountId = ClientAccount.GetNewAccountId();
                user = findClientbyAccountId(accountId);
            } while (user != null);
            Client client = new Client(name, id, new ClientAccount(accountId));
            clientRepository.loadData();
            clientRepository.saveData(client);
            return "0";
        }

    }
}
