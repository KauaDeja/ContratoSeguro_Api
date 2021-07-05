using ContratoSeguro.Comum.Entidades;
using Flunt.Br.Extensions;
using Flunt.Validations;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ContratoSeguro.Dominio.Entidades
{
    public class Documento : Entidade
    {
        //As validações serão feitos em nosso Command
        //private IList<Usuario> _usuario;
        public Documento(string urlArquivo)
        {
            UrlArquivo = urlArquivo;
        }

        public Documento(string nomeDestinatario, string emailDestinatario, DateTime dataExpiracao, Guid idUsuario)
        {
            NomeDestinatario = nomeDestinatario;
            EmailDestinatario = emailDestinatario;
            DataExpiracao = dataExpiracao;
            IdUsuario = idUsuario;
        }

        public string Nome { get; set; }
        public string UrlArquivo { get; set; }
        public virtual Usuario Usuario { get; private set; }
        //Propriedades para adicionar um destinatário
        public string NomeDestinatario { get; set; }
        public string EmailDestinatario { get; set; }
        public DateTime DataExpiracao { get; set; }

        public void AdicionarDestinatario(string nomeDestinatario, string emailDestinatario)
        {
            AddNotifications(new Contract()
               .Requires()
               .HasMinLen(nomeDestinatario, 3, "NomeDestinatario", "Nome deve conter pelo menos 3 caracteres")
               .HasMaxLen(nomeDestinatario, 40, "NomeDestinatario", "Nome deve conter até 40 caracteres")
               .IsEmail(emailDestinatario, "EmailDestinatario", "Informe um e-mail válido")
           );

            if (Valid)
            {
                NomeDestinatario = nomeDestinatario;
                EmailDestinatario = emailDestinatario;
            }

        }

        //public void AdicionarDestinatario(Usuario usuario)
        //{
        //    _usuario.Add(usuario);
        //}

    }


}
