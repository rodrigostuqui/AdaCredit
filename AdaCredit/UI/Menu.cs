using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;
using AdaCredit.UI.UseCases;
using AdaCredit.Services;

namespace AdaCredit.UI
{
    public class Menu : Window
    {

        public Menu()
        {

            Title = "AdaCredit - Menu";

            var menu = new MenuBar(new MenuBarItem[] {
            new MenuBarItem ("Cliente", new MenuItem [] {
                new MenuItem ("Cadastrar Novo Cliente", "", () => { CreateClient.Run(); }, null, null, Key.AltMask | Key.CtrlMask | Key.D),
                new MenuItem ("Consultar os Dados de um Cliente", "", () => { ConsultClient.Run(); }, null, null, Key.AltMask | Key.CtrlMask| Key.N),
                new MenuItem ("Alterar o Cadastro de um Cliente", "", () => { UpdateClient.Run(); }, null, null, Key.AltMask | Key.CtrlMask| Key.O),
                new MenuItem ("Desativar Cadastro de um Cliente", "", () => { DeactivateClient.Run(); }, null, null, Key.AltMask | Key.CtrlMask | Key.H)
            }),
            new MenuBarItem ("Funcionario", new MenuItem [] {
                new MenuItem ("Cadastrar Novo Funcionario", "", () => { CreateEmployee.Run(); }, null, null, Key.AltMask | Key.CtrlMask | Key.D),
                new MenuItem ("Alterar o Cadastro de um Funcionario", "", () => { UpdateEmployee.Run(); }, null, null, Key.AltMask | Key.CtrlMask| Key.O),
                new MenuItem ("Desativar Cadastro de um Funcionario", "", () => { DeactivateEmployee.Run(); }, null, null, Key.AltMask | Key.CtrlMask | Key.H)
            }),
            new MenuBarItem ("Transações", new MenuItem [] {
                new MenuItem ("Processar Transações", "", () => { TransactionServices.ProcessTransactions(); }, null, null, Key.AltMask | Key.CtrlMask | Key.D)
            }),
            new MenuBarItem ("Relatórios", new MenuItem [] {
                new MenuItem ("Exibir Todos os Clientes Ativos", "", () => { ReportClient.Active(); }, null, null, Key.AltMask | Key.CtrlMask | Key.D),
                new MenuItem ("Exibir Todos os Clientes Inativos", "", () => { ReportClient.Inactive(); }, null, null, Key.AltMask | Key.CtrlMask| Key.N),
                new MenuItem ("Exibir Todos os Funcionários Ativos", "", () => { ReportEmployee.Run(); }, null, null, Key.AltMask | Key.CtrlMask| Key.O),
                new MenuItem ("Exibir Transações com Erro", "", () => { ReportFailedTransactions.Run(); }, null, null, Key.AltMask | Key.CtrlMask | Key.H)
            }),
            new MenuBarItem("Sair", new MenuItem []
            {
                new MenuItem ("Deslogar", "", () => { Application.RequestStop(); Application.Run<Login>(); }, null, null, Key.AltMask),
                new MenuItem ("Fechar Programa", "", () => { Application.RequestStop(); }, null, null, Key.AltMask)
            })});

            var fakerButton = new Button()
            {
                Text = "Criar Clientes(Bogus)",
                X = 2,
                Y = 2,
            };
            fakerButton.Clicked += () =>
            {
                ClientServices.CreateWithFaker();
                MessageBox.Query("Clientes Cadastrados", "Foram cadastrados 15 Clientes", "ok");
            };

            Add(menu, fakerButton);
        }
    }
}
