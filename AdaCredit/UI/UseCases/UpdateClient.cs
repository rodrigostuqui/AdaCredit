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
    public class UpdateClient
    {
        public static void Run()
        {
            var cpfLabel = new Label()
            {
                Text = "Cpf: ",
            };
            var cpfText = new TextField("")
            {
                X = Pos.Right(cpfLabel) + 1,
                Width = Dim.Fill(),
            };
            var nameLabel = new Label()
            {
                Text = "Novo Nome: ",
                Y = Pos.Bottom(cpfLabel) + 1,
            };
            var nameText = new TextField("")
            {
                X = Pos.Right(nameLabel) + 1,
                Y = Pos.Bottom(cpfText) + 1,
                Width = Dim.Fill(),
            };
            var ok = new Button("Ok", is_default: true);
            ok.Clicked += () =>
            {
                var numberError = ClientServices.UpdateClientInfo((string)cpfText.Text, (string)nameText.Text);
                if (numberError == "0")
                {
                    MessageBox.Query("Cadastro Atualizado", "Senha Atualizada com Sucesso", "Ok");
                    Application.Shutdown();
                    Application.Run<Menu>();
                }
                else
                {
                    PersonValidation.InvalidCpf();
                }
            };
            var cancel = new Button("Cancel");
            cancel.Clicked += () => { Application.RequestStop(); };
            var d = new Dialog("Cadastrar Funcionario", 60, 20, ok, cancel);
            d.Add(cpfLabel, cpfText, nameLabel, nameText);
            Application.Run(d);
        }

    }
}
