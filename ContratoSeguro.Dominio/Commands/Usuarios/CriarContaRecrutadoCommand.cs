using ContratoSeguro.Comum.Commands;
using ContratoSeguro.Comum.Entidades;
using ContratoSeguro.Comum.Enum;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContratoSeguro.Dominio.Command.Usuarios
{
    public class CriarContaRecrutadoCommand : Notifiable, ICommand
    {
        //Atributos Recrutado
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Telefone { get; set; }
        public string CPF { get; set; }
        public EnTipoUsuario Tipo { get; set; }

        public CriarContaRecrutadoCommand(string nome, string email, string senha, string telefone, string cPF, EnTipoUsuario tipo)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            Telefone = telefone;
            CPF = cPF;
            Tipo = tipo;
        }

        public void Validar()
        {
            AddNotifications(new Contract()
                .Requires()
                .HasMinLen(Nome, 3, "Nome", "Nome deve conter pelo menos 3 caracteres.")
                .HasMaxLen(Nome, 40, "Nome", "Nome deve conter no máximo 40 caracteres.")
                .IsEmail(Email, "Email", "Informe um e-mail válido")
                .HasMinLen(Senha, 6, "Senha", "Senha deve ter no minímo 6 caracteres.")
                .HasMinLen(CPF, 11, "CPF", "O seu numero de CPF deve conter no minimo e no maximo 11 digitos.")
            );
        }
        
    }
}
