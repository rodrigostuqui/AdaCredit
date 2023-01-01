using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace AdaCredit.UI.Exceptions
{
    public class EmployeeExceptions
    {
        public static void LoginError()
        => MessageBox.Query("Login Invalido", "O login/Senha Está Invalido", "Ok");

        public static void usedLogin()
        => MessageBox.Query("User Invalido", "O nome de usuario já foi cadastrado", "Ok");

        public static void DeactivateUser()
        => MessageBox.Query("Usuario Desativado", "O usuario está desativado no banco de dados", "Ok");

    }
}
