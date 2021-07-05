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
    public class AlterarSenhaCommand : Notifiable, ICommand
    {
        public AlterarSenhaCommand(Guid idUsuario, string senhaAtual, string novaSenha)
        {
            IdUsuario = idUsuario;
            SenhaAtual = senhaAtual.Trim();
            NovaSenha = novaSenha.Trim();
        }
        public Guid IdUsuario { get; set; }
        public string SenhaAtual { get;  set; }
        public string NovaSenha { get; set; }
        public void Validar()
        {
            AddNotifications(new Contract()
                .Requires()
                .AreNotEquals(IdUsuario, Guid.Empty, "IdUsuario", "Informe um id de usuário valido")
                .HasMinLen(SenhaAtual, 6, "Senha", "A senha deve conter no minimo 6 caracteres")
                .HasMinLen(NovaSenha, 6, "NovaSenha", "A senha deve conter no minimo 6 caracteres")
                );
        }
    }
}
