using AdaCredit.Domain;
using AdaCredit.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml;
using Terminal.Gui;
namespace AdaCredit.UI.UseCases
{
    public class ReportClient
    {
        public static void Active()
        {
            DataTable dt = new DataTable();
            var data = ClientServices.getAllActiveClients();
            dt.Columns.Add("Nome", typeof(int));
            dt.Columns.Add("Cpf", typeof(int));
            dt.Columns.Add("Conta", typeof(int));
            dt.Columns.Add("Saldo", typeof(int));

            for (int i = 0; i < data.Count; i++)
            {
                dt.Rows.Add(i, i, i, i);
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
                .RepresentationGetter = (x) => data[(int)x].Id;

            tableView.Style.GetOrCreateColumnStyle(dt.Columns["Conta"])
                .RepresentationGetter = (x) => data[(int)x].Account.GetHashCode();

            tableView.Style.GetOrCreateColumnStyle(dt.Columns["Saldo"])
                .RepresentationGetter = (x) => $"{data[(int)x].Account.Balance}";

            var ok = new Button("Ok");

            ok.Clicked += () =>
            {
                Application.RequestStop();
            };
            var cancel = new Button("Cancel");
            cancel.Clicked += () => { Application.RequestStop(); };
            var d = new Dialog("Clientes Ativos", 90, 30, ok);
            d.Add(tableView);
            Application.Run(d);

        }

        public static void Inactive()
        {
            DataTable dt = new DataTable();
            var data = ClientServices.getAllInactiveClients();
            dt.Columns.Add("Nome", typeof(int));
            dt.Columns.Add("Cpf", typeof(int));
            dt.Columns.Add("Conta", typeof(int));

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
                .RepresentationGetter = (x) => data[(int)x].Id;

            tableView.Style.GetOrCreateColumnStyle(dt.Columns["Conta"])
                .RepresentationGetter = (x) => data[(int)x].Account.GetHashCode();
            
            var ok = new Button("Ok");
            ok.Clicked += () =>
            {
                Application.RequestStop();
            };
            var cancel = new Button("Cancel");
            cancel.Clicked += () => { Application.RequestStop(); };
            var d = new Dialog("Clientes Inativos", 90, 30, ok);
            d.Add(tableView);
            Application.Run(d);

        }
    }
}
