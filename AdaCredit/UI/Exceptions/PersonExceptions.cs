using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace AdaCredit.UI.Exceptions
{
    public class PersonExceptions
    {
        public static void InvalidCpf()
            => MessageBox.Query("Cpf Invalido", "O cpf não foi encontrado na base de dados", "Ok");

        public static void usedId()
            => MessageBox.Query("Cpf Invalido", "O cpf já foi cadastrado", "Ok");
    }
}
