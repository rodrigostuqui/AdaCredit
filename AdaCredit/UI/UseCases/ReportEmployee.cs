using AdaCredit.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace AdaCredit.UI.UseCases
{
    public class ReportEmployee
    {
        public static void Run()
        {
            DataTable dt = new DataTable();
            var data = EmployeeServices.GetActiveEmployees();
            dt.Columns.Add("Nome", typeof(int));
            dt.Columns.Add("Cpf", typeof(int));
            dt.Columns.Add("Ultimo Login", typeof(int));

            for (int i = 0; i < data.Count; i++)
            {
                dt.Rows.Add(i, i, i);
            }

            var tableView = new TableView()
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill(1),
            };
            tableView.Table = dt;

            tableView.Style.GetOrCreateColumnStyle(dt.Columns["Nome"])
                .RepresentationGetter = (x) => data[(int)x].Name;

            tableView.Style.GetOrCreateColumnStyle(dt.Columns["Cpf"])
                .RepresentationGetter = (x) => data[(int)x].GetHashCode();

            tableView.Style.GetOrCreateColumnStyle(dt.Columns["Ultimo Login"])
                .RepresentationGetter = (x) => $"{data[(int)x].LastVisit}";


            var ok = new Button("Ok");
            ok.Clicked += () =>
            {
                Application.RequestStop();
            };
            var d = new Dialog("Funcionarios ativos", 90, 30, ok);
            d.Add(tableView);
            Application.Run(d);

        }
    }
}
