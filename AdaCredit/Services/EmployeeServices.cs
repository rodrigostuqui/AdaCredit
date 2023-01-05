using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdaCredit.Domain;
using AdaCredit.Data;
using BC = BCrypt.Net.BCrypt;
using AdaCredit.UI.Exceptions;

namespace AdaCredit.Services
{
    public class EmployeeServices
    {
        static Repository<Employee> employeeRepository = new Repository<Employee>("employee.csv");

        public static int RepositoryLength()
        {
            employeeRepository.loadData();
            return employeeRepository.Count();
        }

        public static Employee findEmployeeById(string employeeId)
        {
            employeeRepository.loadData();
            var user = employeeRepository._data.FirstOrDefault(x => x.GetHashCode() == employeeId);
            return user;
        }


        public static Employee findEmployeeByLogin(string login)
        {
            employeeRepository.loadData();
            var user = employeeRepository._data.FirstOrDefault(x => x.Login == login);
            return user;
        }

        public static string CreateEmployee(string name, string id, string login, string pass)
        {

            if (findEmployeeById(id) != null)
            {
                return "1";
            }
            else if (findEmployeeByLogin(login) != null)
            {
                return "2";
            }
            Employee employee = new Employee(name, id, login, pass);
            employeeRepository.loadData();
            employeeRepository.saveData(employee);
            return "0";
        }

        public static string ChangeEmployeeStatus(string employeeId, bool status)
        {
            employeeRepository.loadData();
            var user = findEmployeeById(employeeId);
            if (user != null) 
            {
                user.Status = status;
                employeeRepository.UpdateData();
                return "0";
            }
            else
            {
                return "1";
            }
        }

        public static List<Employee> GetActiveEmployees()
        {
            employeeRepository.loadData();
            return employeeRepository._data.Where(x => x.Status == true).ToList();
        }

        public static string Auth(string login, string pass)
        {
            employeeRepository.loadData();
            var user = employeeRepository._data.FirstOrDefault(x => BC.Verify(pass, x.Password) && x.Login == login);
            if (user == null)
            {
                return "3";
            }
            else if(user.Status == false)
            {
                return "4";
            }
            user.UpdateLastVisit();
            employeeRepository.UpdateData();
            return "0";
        }
        public static string UpdateEmployeePass(string Id, string newPass)
        {
            var user = findEmployeeById(Id);
            if (user == null)
            {
                return "1";
            }
            else
            {
                user.UpdatePassword(newPass);
                employeeRepository.UpdateData();
                return "0";
            }
        }
    }
}
