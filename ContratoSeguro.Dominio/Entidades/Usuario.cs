using ContratoSeguro.Comum.Entidades;
using ContratoSeguro.Comum.Enum;
using Flunt.Br.Extensions;
using Flunt.Validations;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ContratoSeguro.Dominio.Entidades
{
    public class Usuario : Entidade
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public EnTipoUsuario Tipo { get; set; }
        public string Telefone { get; set; }
        public List<Documento> Documentos { get; set; }

        [NotMapped]
        public IFormFile Arquivo { get;  set; }
        public string UrlFoto { get; set; }


        public void AdicionarTelefone(string telefone)
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNewFormatCellPhone(telefone, "Telefone", "Informe um telefone válido")
                );
            if (Valid)
                Telefone = telefone;
        }


        public void AlterarSenha(string senha)
        {
            AddNotifications(new Contract()
                   .Requires()
                .HasMinLen(senha, 6, "Senha", "A senha deve ter pelo menos 6 caracteres")
                    );
            if (Valid)
                Senha = senha;

        }

        public void AlterarNome(string nome)
        {
            AddNotifications(new Contract()
                   .Requires()
               .HasMinLen(nome, 3, "Nome", "Nome deve conter pelo menos 3 caracteres.")
               .HasMaxLen(nome, 40, "Nome", "Nome deve conter no máximo 40 caracteres.")
                    );
            if (Valid)
                Nome = nome;

        }
        public void AlterarImagem(string urlFoto)
        {
                UrlFoto = urlFoto;

        }

    }

 }

