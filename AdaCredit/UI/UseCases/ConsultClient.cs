using AdaCredit.Services;
using AdaCredit.UI.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace AdaCredit.UI.UseCases
{
    public class ConsultClient
    {
        public static void Run()
        {
            string clientInformations = "";
            var cpfLabel = new Label()
            {
                Text = "Cpf: ",
            };


            var cpfText = new TextField("")
            {
                X = Pos.Right(cpfLabel) + 1,
                Width = Dim.Fill(),
            };

            var search = new Button("Pesquisar");

            var close = new Button("Fechar");

            var d = new Dialog("Cadastrar Funcionario", 60, 20, search, close);

            search.Clicked += () =>
            {
                clientInformations = ClientServices.getAllInfo((string)cpfText.Text);
                if (clientInformations == "1")
                {
                    PersonValidation.InvalidCpf();
                }
                else
                {
                    var infoLabel = new Label()
                    {
                        Y = 4,
                        X = 4,
                        Text = clientInformations,
                    };
                    d.Add(infoLabel, cpfLabel, cpfText);
                }
            };
            close.Clicked += () =>
            {
                Application.RequestStop();
                Application.Run<Menu>();
            };
            d.Add(cpfLabel, cpfText);
            Application.Run(d);

        }

    }
}
