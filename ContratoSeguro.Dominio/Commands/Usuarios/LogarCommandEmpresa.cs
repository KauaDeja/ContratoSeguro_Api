using ContratoSeguro.Comum.Commands;
using Flunt.Br.Extensions;
using Flunt.Notifications;
using Flunt.Validations;

namespace ContratoSeguro.Dominio.Commands.Usuarios
{
    public class LogarCommandEmpresa : Notifiable, ICommand
    {
        public LogarCommandEmpresa(string cNPJ, string email, string senha)
        {
            Email = email;
            Senha = senha;
            CNPJ = cNPJ;
        }

        public string Email { get; set; }
        public string Senha { get; set; }
        public string CNPJ { get; set; }
        public void Validar()
        {
            AddNotifications(new Contract()
             .Requires()
             .IsEmail(Email, "Email", "Informe um e-mail válido")
             .HasMinLen(Senha, 6, "Senha", "A senha deve ter pelo menos 6 caracteres")
             .IsCnpj(CNPJ, "Document", "Invalid document")
                );
        }

    }
}