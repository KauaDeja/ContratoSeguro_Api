using ContratoSeguro.Comum.Commands;
using Flunt.Br.Extensions;
using Flunt.Notifications;
using Flunt.Validations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContratoSeguro.Dominio.Commands.Documentos
{
    public class UploadArquivoCommand : Notifiable, ICommand
    {
        public UploadArquivoCommand(string urlArquivo, DateTime dataExpiracao, string nomeDestinatario, string emailDestinatario)
        {
            UrlArquivo = urlArquivo;
        }

        public string UrlArquivo { get; set; }
        public DateTime DataExpiracao { get; set; }
        public string NomeDestinatario { get; set; }
        public string EmailDestinatario { get; set; }

        public void Validar()
        {
            AddNotifications(new Contract()
               .Requires()
               .IsNotNullOrEmpty(UrlArquivo, "Arquivo", "Envie um arquivo")
               .HasMinLen(NomeDestinatario, 3, "NomeDestinatario", "Nome deve conter pelo menos 3 caracteres")
               .HasMaxLen(NomeDestinatario, 40, "NomeDestinatario", "Nome deve conter até 40 caracteres")
               .IsEmail(EmailDestinatario, "EmailDestinatario", "Informe um e-mail válido")
               );
        }
    }
}
