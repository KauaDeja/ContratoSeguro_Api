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
    public class Empresa : Usuario
    {

        //Atributos Empresa
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public string Matriz { get; set; }
        public string Logradouro { get; set; }
        public string UF { get; set; }
        public string Cidade { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public DateTime DataAbertura { get; set; }


        public Empresa(string nome, string email, string senha, string cNPJ, string razaoSocial, string matriz, string logradouro, string uF, string cidade, string numero, string bairro, DateTime dataAbertura, EnTipoUsuario tipo)
        {


            AddNotifications(new Contract()
               .Requires()
               .HasMinLen(nome, 3, "Nome", "Nome deve conter pelo menos 3 caracteres.")
               .HasMaxLen(nome, 40, "Nome", "Nome deve conter no máximo 40 caracteres.")
               .IsEmail(email, "Email", "Informe um e-mail válido")
               .HasMinLen(senha, 6, "Senha", "Senha deve ter no minímo 6 caracteres.")
               .IsCnpj(cNPJ, "CNPJ", "CNPJ inválida")
               .HasMaxLen(matriz, 40, "Matriz", "A matriz  deve conter no máximo 40 caracteres")
               .HasMinLen(numero, 1, "Numero", "O numero deve conter no minimo 1 digito")


               );

            if (Valid)
            {
                Nome = nome;
                Email = email;
                Senha = senha;
                CNPJ = cNPJ;
                RazaoSocial = razaoSocial;
                Matriz = matriz;
                Logradouro = logradouro;
                UF = uF;
                Cidade = cidade;
                Numero = numero;
                Bairro = bairro;
                DataAbertura = dataAbertura;
                Tipo = tipo;
            }

        }

    }
}
