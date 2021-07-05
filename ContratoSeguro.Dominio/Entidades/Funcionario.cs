using ContratoSeguro.Comum.Entidades;
using ContratoSeguro.Comum.Enum;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContratoSeguro.Dominio.Entidades
{
    public class Funcionario : Usuario
    {
        //Atributos Funcionario
        public string CPF { get; set; }
        public string RG { get; set; }
        public string Formação { get; set; }
        public DateTime DataNascimento { get; set; }



        // Notificações - FLunt
        public Funcionario(string nome, string email, string senha,  string cPF, string rG, string formação, DateTime dataNascimento, EnTipoUsuario tipo)
        {

            AddNotifications(new Contract()
                .Requires()
                .HasMinLen(nome, 3, "Nome", "Nome deve conter pelo menos 3 caracteres.")
                .HasMaxLen(nome, 40, "Nome", "Nome deve conter no máximo 40 caracteres.")
                .IsEmail(email, "Email", "Informe um e-mail válido")
                .HasMinLen(senha, 6, "Senha", "Senha deve ter no minímo 6 caracteres.")
                .HasMinLen(cPF, 11, "CPF", "O seu numero de CPF deve conter no minimo e no maximo 11 digitos.")
                .HasMinLen(rG, 9, "RG", "O seu numero de RG deve conter no minimo e no maximo 9 digitos.")
            );

            if (Valid)
            {
                Nome = nome;
                Email = email;
                Senha = senha;
                CPF = cPF;
                RG = rG;
                Formação = formação;
                DataNascimento = dataNascimento;
                Tipo = tipo;
            }

        }

        public void AlterarFormacao(string formacão)
        {
            Formação = formacão;

        }
    }
}
