using AdaCredit.Data;
using AdaCredit.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace AdaCredit.Services
{
    public class TransactionServices
    {
        public static Repository<Transactions> pendingTransactions;

        public static Repository<Transactions> failedTransactions;

        public static Repository<Transactions> completedTransactions;

        public static void ProcessTransactions()
        {
            Client originClient = null;
            Client destinyClient = null;
            var files = Directory.GetFiles($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\Transactions\\Pending");
            foreach (var file in files)
            {
                var filename = Path.GetFileName(file);
                var listFilename = filename.Split(".").ToList();
                listFilename = listFilename[0].Split("-").ToList();
                var data = DateTime.ParseExact(listFilename[listFilename.Count - 1], "yyyyMMdd", CultureInfo.InvariantCulture);
                var filePath = $"Transactions\\Pending\\{filename}";
                pendingTransactions = new Repository<Transactions>(filePath);
                pendingTransactions.loadData();
                for (int i = 0; i < pendingTransactions.Count(); i++)
                {
                    var amount = pendingTransactions._data[i].CalculateTax(data);
                    if (pendingTransactions._data[i].OriginbankId == "777" && pendingTransactions._data[i].OriginAgencyId == "0001")
                    {
                        originClient = ClientServices.findClientbyAccountId(pendingTransactions._data[i].OriginAccountId);
                        if (originClient == null)
                        {
                            addError(pendingTransactions._data[i], listFilename);
                            break;
                        }
                        else
                        {
                            if (originClient.Account.Balance < amount)
                            {
                                addError(pendingTransactions._data[i], listFilename);
                                break;
                            }
                            else
                            {
                                originClient.Account.Withdraw(amount);
                            }
                        }
                    }
                    if (pendingTransactions._data[i].DestinybankId == "777" && pendingTransactions._data[i].DestinyAgencyId == "0001")
                    {
                        destinyClient = ClientServices.findClientbyAccountId(pendingTransactions._data[i].DestinyAccountId);
                        if (destinyClient == null)
                        {
                            addError(pendingTransactions._data[i], listFilename);
                            break;
                        }
                        else
                        {
                            destinyClient.Account.Deposit(pendingTransactions._data[i].TransactionAmount);
                        }
                    }
                    if (pendingTransactions._data[i].TransctionType == "TEF" && (originClient == null || destinyClient == null))
                    {
                        addError(pendingTransactions._data[i], listFilename);
                        break;
                    }
                    ClientServices.clientRepository.UpdateData();
                    addCompleted(pendingTransactions._data[i], listFilename);
                }
                File.Delete($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\{filePath}");
            }
        }
        public static void addError(Transactions transaction, List<string> listFilename)
        {
            failedTransactions = new Repository<Transactions>($"\\Transactions\\Failed\\{string.Join("-", listFilename)}-failed.csv");
            failedTransactions.saveData(transaction);
        }

        public static void addCompleted(Transactions transaction, List<string> listFilename)
        {
            completedTransactions = new Repository<Transactions>($"\\Transactions\\Completed\\{string.Join("-", listFilename)}-completed.csv");
            completedTransactions.saveData(transaction);
        }

        public static List<Transactions> getAllFailedTransactions()
        {
            var listFailed = new List<Transactions>();
            var files = Directory.GetFiles($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\Transactions\\Failed");
            foreach ( var file in files ) 
            {
                failedTransactions = new Repository<Transactions>($"\\Transactions\\Failed\\{Path.GetFileName(file)}");
                foreach(var transaction in failedTransactions._data)
                {
                    listFailed.Add(transaction);
                }
            }
            return listFailed;
        }
    }
}