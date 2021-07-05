using ContratoSeguro.Comum.Commands;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContratoSeguro.Dominio.Commands.Usuarios
{
    public class DeletarFuncionarioCommand : Notifiable, ICommand
    {
        public void Validar()
        {

        }

        public Guid IdFuncionario { get; set; }
    }
}
