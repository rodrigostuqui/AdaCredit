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
    public class ReportFailedTransactions
    {
        public static void Run()
        {
            DataTable dt = new DataTable();
            var data = TransactionServices.getAllFailedTransactions();
            dt.Columns.Add("Banco Origem", typeof(int));
            dt.Columns.Add("Agencia Origem", typeof(int));
            dt.Columns.Add("Conta Origem", typeof(int));
            dt.Columns.Add("Banco Destino", typeof(int));
            dt.Columns.Add("Agencia Destino", typeof(int));
            dt.Columns.Add("Conta Destino", typeof(int));
            dt.Columns.Add("Valor", typeof(int));

            for (int i = 0; i < data.Count; i++)
            {
                dt.Rows.Add(i, i, i, i, i, i, i);
            }

            var tableView = new TableView()
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill(1),
            };
            tableView.Table = dt;

            tableView.Style.GetOrCreateColumnStyle(dt.Columns["Banco Origem"])
                .RepresentationGetter = (x) => data[(int)x].OriginbankId;

            tableView.Style.GetOrCreateColumnStyle(dt.Columns["Agencia Origem"])
                .RepresentationGetter = (x) => data[(int)x].OriginAgencyId;

            tableView.Style.GetOrCreateColumnStyle(dt.Columns["Conta Origem"])
                .RepresentationGetter = (x) => data[(int)x].OriginAccountId;

            tableView.Style.GetOrCreateColumnStyle(dt.Columns["Banco Destino"])
    .RepresentationGetter = (x) => data[(int)x].DestinybankId;

            tableView.Style.GetOrCreateColumnStyle(dt.Columns["Agencia Destino"])
                .RepresentationGetter = (x) => data[(int)x].DestinyAgencyId;

            tableView.Style.GetOrCreateColumnStyle(dt.Columns["Conta Destino"])
                .RepresentationGetter = (x) => data[(int)x].DestinyAccountId;

            tableView.Style.GetOrCreateColumnStyle(dt.Columns["Valor"])
                .RepresentationGetter = (x) => $"{data[(int)x].TransactionAmount}";


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
