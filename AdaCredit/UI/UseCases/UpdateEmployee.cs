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
    public class UpdateEmployee
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
            var newPassLabel = new Label()
            {
                Text = "Nova Senha: ",
                Y = Pos.Bottom(cpfLabel) + 1,
            };
            var newPassText = new TextField("")
            {
                X = Pos.Right(newPassLabel) + 1,
                Y = Pos.Bottom(cpfText) + 1,
                Width = Dim.Fill(),
                Secret = true,
            };
            var ok = new Button("Ok", is_default: true);
            ok.Clicked += () =>
            {
                if (EmployeeServices.UpdateEmployeePass((string)cpfText.Text, (string)newPassText.Text) == 0)
                {
                    MessageBox.Query("Cadastro Atualizado", "Senha Atualizada com Sucesso", "Ok");
                    Application.Shutdown();
                    Application.Run<Menu>();
                }
                else
                {
                    PersonExceptions.InvalidCpf();
                }
            };
            var cancel = new Button("Cancel");
            cancel.Clicked += () => { Application.RequestStop(); };
            var d = new Dialog("Cadastrar Funcionario", 60, 20, ok, cancel);
            d.Add(cpfLabel, cpfText, newPassLabel, newPassText);
            Application.Run(d);
        }

    }
}
