using ContratoSeguro.Comum.Commands;
using Flunt.Br.Extensions;
using Flunt.Notifications;
using Flunt.Validations;

namespace ContratoSeguro.Dominio.Commands.Usuarios
{
    public class LogarCommandRecrutado : Notifiable, ICommand
    {
        public LogarCommandRecrutado(string cPF, string email, string senha)
        {
            Email = email;
            Senha = senha;
            CPF = cPF;
        }

        public string Email { get; set; }
        public string Senha { get; set; }
        public string CPF { get; set; }
        public void Validar()
        {
            AddNotifications(new Contract()
             .Requires()
             .IsEmail(Email, "Email", "Informe um e-mail válido")
             .HasMinLen(Senha, 6, "Nome", "A senha deve ter pelo menos 6 caracteres")
             .IsCpf(CPF, "CPF", "CPF inválido")

                );
        }

    }
}