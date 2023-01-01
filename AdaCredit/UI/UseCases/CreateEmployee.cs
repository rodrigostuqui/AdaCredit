using AdaCredit.Services;
using AdaCredit.UI;
using AdaCredit.UI.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace AdaCredit.UI.UseCases
{
    public static class CreateEmployee
    {
        public static void Run()
        {
            var nameLabel = new Label()
            {
                Text = "Name:         "
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
            var usernameLabel = new Label()
            {
                Text = "Novo usuario: ",
                Y = Pos.Bottom(cpfLabel) + 1
            };
            var usernameText = new TextField("")
            {
                X = Pos.Right(usernameLabel) + 1,
                Y = Pos.Bottom(cpfLabel) + 1,
                Width = Dim.Fill(),

            };
            var passLabel = new Label()
            {
                Text = "Nova Senha:   ",
                Y = Pos.Bottom(usernameLabel) + 1,
            };
            var passText = new TextField("")
            {
                X = Pos.Right(passLabel) + 1,
                Y = Pos.Bottom(usernameText) + 1,
                Width = Dim.Fill(),
                Secret = true,
            };
            var ok = new Button("Ok", is_default: true);
            ok.Clicked += () =>
            {
                var errorNumber = EmployeeServices.CreateEmployee((string)nameText.Text, (string)cpfText.Text, (string)usernameText.Text, (string)passText.Text);
                switch (errorNumber)
                {
                    case 0:
                        MessageBox.Query("Cadastro Atualizado", "Cadastro Feito com Sucesso", "Ok");
                        Application.RequestStop();
                        Application.Run<Menu>();
                        break;
                    case 1:
                        PersonExceptions.usedId();
                        break;
                    case 2:
                        EmployeeExceptions.usedLogin();
                        break;
                }
            };
            var cancel = new Button("Cancel");
            cancel.Clicked += () => { Application.RequestStop(); };
            var d = new Dialog("Cadastrar Funcionario", 60, 20, ok, cancel);
            d.Add(nameLabel, nameText, cpfText, cpfLabel, usernameLabel, usernameText, passLabel, passText);
            Application.Run(d);
        }
    }
}
