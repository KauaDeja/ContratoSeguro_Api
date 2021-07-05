using ContratoSeguro.Comum.Commands;
using Flunt.Notifications;
using Flunt.Validations;
using System;


namespace ContratoSeguro.Dominio.Commands.Usuarios
{
    public class AlterarNomeCommand : Notifiable, ICommand
    {
        public AlterarNomeCommand(Guid idUsuario, string nome)
        {
            IdUsuario = idUsuario;
            Nome = nome;
        }
        public Guid IdUsuario { get; set; }
        public string Nome { get; set; }
        public void Validar()
        {
            AddNotifications(new Contract()
                .Requires()
                .AreNotEquals(IdUsuario, Guid.Empty, "IdUsuario", "Informe um id de usuário valido")
               .HasMinLen(Nome, 3, "Nome", "Nome deve conter pelo menos 3 caracteres.")
               .HasMaxLen(Nome, 40, "Nome", "Nome deve conter no máximo 40 caracteres.")
                );
        }
    }
}
