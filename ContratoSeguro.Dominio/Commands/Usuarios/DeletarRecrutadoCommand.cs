


using ContratoSeguro.Comum.Commands;
using Flunt.Notifications;
using System;

namespace ContratoSeguro.Dominio.Commands.Usuarios
{
    public class DeletarRecrutadoCommand : Notifiable, ICommand
    {
        public void Validar()
        {
            
        }

        public Guid IdRecrutado { get; set; }
    }
}
