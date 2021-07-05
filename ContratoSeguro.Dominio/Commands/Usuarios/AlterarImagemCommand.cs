using ContratoSeguro.Comum.Commands;
using Flunt.Notifications;
using Flunt.Validations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContratoSeguro.Dominio.Commands.Usuarios
{
    public class AlterarImagemCommand : Notifiable, ICommand
    {
        public AlterarImagemCommand()
        {

        }
        public AlterarImagemCommand(Guid idUsuario, IFormFile arquivo)
        {
            IdUsuario = idUsuario;
            Arquivo = arquivo;
        }
        public Guid IdUsuario { get; set; }
        public IFormFile Arquivo { get; set; }
        public void Validar()
        {
            AddNotifications(new Contract()
                .Requires()
                .AreNotEquals(IdUsuario, Guid.Empty, "IdUsuario", "Informe um id de usuário valido")
                );
        }
    }
}
