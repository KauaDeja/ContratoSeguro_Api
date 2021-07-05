using ContratoSeguro.Comum.Entidades;
using ContratoSeguro.Comum.Enum;
using Flunt.Br.Extensions;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContratoSeguro.Dominio.Entidades
{
    public class Recrutado : Usuario
    {
        //Atributos Recrutado
        public List<Recrutado> _recrutado { get; }
        public string CPF { get; set; }
        


        public Recrutado(string nome, string email, string senha, string cPF, EnTipoUsuario tipo)
        {
            AddNotifications(new Contract()
                .Requires()
                .HasMinLen(nome, 3, "Nome", "Nome deve conter pelo menos 3 caracteres.")
                .HasMaxLen(nome, 40, "Nome", "Nome deve conter no máximo 40 caracteres.")
                .IsEmail(email, "Email", "Informe um e-mail válido")
                .HasMinLen(senha, 6, "Senha", "Senha deve ter no minímo 6 caracteres.")
                .IsCpf(cPF, "CPF", "CPF inválido")

            );

            if (Valid)
            {
                Nome = nome;
                Email = email;
                Senha = senha;
                CPF = cPF;
                Tipo = tipo;
            }

        } 
    
    }

}
