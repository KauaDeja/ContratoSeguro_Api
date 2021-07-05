using ContratoSeguro.Comum.Commands;
using ContratoSeguro.Comum.Enum;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContratoSeguro.Dominio.Commands.Usuarios
{
    public class CriarContaEmpresaCommand : Notifiable, ICommand
    {
        public CriarContaEmpresaCommand(string nome, string email, string senha, string telefone, string cNPJ, EnTipoUsuario tipo, string razaoSocial, string matriz, string logradouro, string uF, string cidade, string numero, string bairro, DateTime dataAbertura)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            Telefone = telefone;
            CNPJ = cNPJ;
            Tipo = tipo;
            RazaoSocial = razaoSocial;
            Matriz = matriz;
            Logradouro = logradouro;
            UF = uF;
            Cidade = cidade;
            Numero = numero;
            Bairro = bairro;
            DataAbertura = dataAbertura;
        }

        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Telefone { get; set; }
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public string Matriz { get; set; }
        public string Logradouro { get; set; }
        public string UF { get; set; }
        public string Cidade { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public DateTime DataAbertura { get; set; }
        public EnTipoUsuario Tipo { get; set; }

        public void Validar()
        {

            AddNotifications(new Contract()
               .Requires()
               .HasMinLen(Nome, 3, "Nome", "Nome deve conter pelo menos 3 caracteres.")
               .HasMaxLen(Nome, 40, "Nome", "Nome deve conter no máximo 40 caracteres.")
               .IsEmail(Email, "Email", "Informe um e-mail válido")
               .HasMinLen(Senha, 6, "Senha", "Senha deve ter no minímo 6 caracteres.")
               .HasMinLen(CNPJ, 14, "CNPJ", "O seu numero de CNPJ deve conter no minimo e no maximo 14 digitos.")
               .HasMaxLen(Matriz, 40, "Matriz", "A matriz  deve conter no máximo 40 caracteres")
               .HasMinLen(Numero, 1, "Numero", "O numero deve conter no minimo 1 digito")


               );
        }
    }
}
