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
    public class CreateClient
    {
        public static void Run()
        {
            var nameLabel = new Label()
            {
                Text = "Nome:         "
            };

            var nameText = new TextField("")
            {
                X = Pos.Right(nameLabel) + 1,

                Width = Dim.Fill()
            };
            var cpfLabel = new Label()
            {
                Text = "CPF:          ",
                Y = Pos.Bottom(nameLabel) + 1
            };
            var cpfText = new TextField("")
            {
                X = Pos.Right(cpfLabel) + 1,
                Y = Pos.Bottom(nameLabel) + 1,
                Width = Dim.Fill()
            };
            var passLabel = new Label()
            {
                Text = "Nova Senha:   ",
                Y = Pos.Bottom(cpfLabel) + 1,
            };
            var passText = new TextField("")
            {
                X = Pos.Right(passLabel) + 1,
                Y = Pos.Bottom(cpfText) + 1,
                Width = Dim.Fill(),
                Secret = true,
            };
            var ok = new Button("Ok", is_default: true);
            ok.Clicked += () =>
            {
                var errorNumber = ClientServices.CreateClient((string) nameText.Text, (string)cpfText.Text, (string)passText.Text);
                if(errorNumber == 1)
                {
                    PersonExceptions.usedId();
                }
                else
                {
                    var accountId = ClientServices.findAccountIdById((string)cpfText.Text);
                    MessageBox.Query("Cadastro Atualizado", $"Cadastro Feito com Sucesso\nNumero da conta: {accountId}\nNumero da Agencia: 0001\n Codigo Bancario: 777", "Ok");
                    Application.RequestStop();
                    Application.Run<Menu>();
                }
            };
            var cancel = new Button("Cancel");
            cancel.Clicked += () => { Application.RequestStop(); };
            var d = new Dialog("Cadastrar Funcionario", 60, 20, ok, cancel);
            d.Add(nameLabel, nameText, cpfText, cpfLabel, passLabel, passText);
            Application.Run(d);
        }
    }
}
