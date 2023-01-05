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
    public static class DeactivateClient
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
            var deactivate = new Button("Desativar");
            deactivate.Clicked += () =>
            {
                var numberError = ClientServices.ChangeEmployeeStatus((string)cpfText.Text, false);
                if (numberError == "0")
                {
                    MessageBox.Query("Cadastro Atualizado", "Status atualizado com sucesso", "Ok");
                    Application.Shutdown();
                    Application.Run<Menu>();
                }
                else
                {
                    PersonValidation.InvalidCpf();
                }
            };
            var activate = new Button("Ativar");
            activate.Clicked += () =>
            {
                var numberError = ClientServices.ChangeEmployeeStatus((string)cpfText.Text, true);
                if (numberError == "0")
                {
                    MessageBox.Query("Cadastro Atualizado", "Status atualizado com sucesso", "Ok");
                    Application.Shutdown();
                    Application.Run<Menu>();
                }
                else
                {
                    PersonValidation.InvalidCpf();
                }
            };
            var d = new Dialog("Cadastrar Funcionario", 60, 20, activate, deactivate);
            d.Add(cpfLabel, cpfText);
            Application.Run(d);
        }
    }
}
