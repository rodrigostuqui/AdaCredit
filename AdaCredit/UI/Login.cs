using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;
using AdaCredit.UI.UseCases;
using AdaCredit.Services;
using AdaCredit.UI.Exceptions;

namespace AdaCredit.UI
{
    public class Login : Window
    {
        TextField usernameText;
        TextField passwordText;
        public Login()
        {
            Title = "AdaCredit - Login";

            var usernameLabel = new Label()
            {
                Text = "User: "
            };

            usernameText = new TextField("")
            {
                X = Pos.Right(usernameLabel) + 1,

                Width = Dim.Fill(),
            };

            var passwordLabel = new Label()
            {
                Text = "Senha:",
                X = Pos.Left(usernameLabel),
                Y = Pos.Bottom(usernameLabel) + 1
            };

            passwordText = new TextField("")
            {
                Secret = true,
                X = Pos.Right(passwordLabel) + 1,
                Y = Pos.Top(passwordLabel),
                Width = Dim.Fill(),
            };

            var btnLogin = new Button()
            {
                Text = "Login",
                Y = Pos.Bottom(passwordLabel) + 1,
                X = Pos.Center(),
            };

            btnLogin.Clicked += () =>
            {
                VerifyLogin();
            };

            Add(usernameLabel, usernameText, passwordLabel, passwordText, btnLogin);
        }


        public void VerifyLogin()
        {
            if (EmployeeServices.RepositoryLength() > 0)
            {
                var errorNumber = EmployeeServices.Auth((string)usernameText.Text, (string)passwordText.Text);
                switch (errorNumber)
                {
                    case "0":
                        Application.RequestStop();
                        Application.Run<Menu>();
                        break;
                    case "3":
                        EmployeeValidation.LoginError();
                        break;
                    case "4":
                        EmployeeValidation.DeactivateUser();
                        break;
                }
            }
            else
            {
                if (usernameText.Text == "user" && passwordText.Text == "pass")
                {
                    CreateEmployee.Run();
                }
                else
                {
                    EmployeeValidation.LoginError();
                }
            }
        }

        public static void Show()
        {
            Application.Run<Login>();
        }
    }
}
