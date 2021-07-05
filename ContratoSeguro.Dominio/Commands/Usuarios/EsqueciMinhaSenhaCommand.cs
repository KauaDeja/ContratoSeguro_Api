using ContratoSeguro.Comum.Commands;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContratoSeguro.Dominio.Commands.Usuarios
{
    public class EsqueciMinhaSenhaCommand : Notifiable, ICommand
    {
        public EsqueciMinhaSenhaCommand()
        {

        }

        public EsqueciMinhaSenhaCommand(string email)
        {
            Email = email;
        }

        public string Email { get; set; }

        public void Validar()
        {
            AddNotifications(new Contract()
               .Requires()
               .IsEmail(Email, "Email", "Informe um e-mail válido")
           );
        }
    }
}
