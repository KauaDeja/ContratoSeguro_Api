using ContratoSeguro.Comum.Commands;
using ContratoSeguro.Comum.Handlers;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ContratoSeguro.Comum.Utills
{
    public class EnviarEmailUsuario 
    {
        public interface IMailService
        {
            Task SendEmailAsync(string toEmail, string subject, string content );
            Task SendEmailAsyncCompany(string toEmail, string subject, string content);
            Task SendEmailAsyncEmployee(string toEmail, string subject, string content);
            Task SendEmailAsyncRecruted(string toEmail,  string subject,  string content);

        }
        public class SendGridMailService : IMailService
        {
            private IConfiguration _configuration;

            public SendGridMailService(IConfiguration configuration)
            {
                _configuration = configuration;
            }
            public async Task SendEmailAsync(string toEmail, string subject, string content )
            {
                var apiKey = "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";
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("contratoseguro@gmail.com", "Contrato Seguro");
                var to = new EmailAddress("renatarecrutada@gmail.com", "Renata");
                var msg = MailHelper.CreateSingleEmail(from, to, subject, content, null);
                var response = await client.SendEmailAsync(msg);
            }
            //Empresa
            public async Task SendEmailAsyncCompany(string toEmail, string subject, string content)
            {
                var apiKey = "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";
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("contratoseguro@gmail.com", "Contrato Seguro");
                var to = new EmailAddress("tacosadvocacia@gmail.com", "Tacos");
                var msg = MailHelper.CreateSingleEmail(from, to, subject, content, null);
                var response = await client.SendEmailAsync(msg);
            }

            //Funcionario
            public async Task SendEmailAsyncEmployee(string toEmail, string subject, string content )
            {
                var apiKey = "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";
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("contratoseguro@gmail.com", "Contrato Seguro");
                var to = new EmailAddress("joaofuncionariologin@gmail.com", "Joao Augusto");
                var msg = MailHelper.CreateSingleEmail(from, to, subject, content, null);
                var response = await client.SendEmailAsync(msg);
            }

            //Recrutado
            public async Task SendEmailAsyncRecruted(string toEmail, string subject ,string content)
            {  
                var apiKey = "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";
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("contratoseguro@gmail.com", "Contrato Seguro");
                var to = new EmailAddress("renatarecrutada@gmail.com", "Renata");
                var msg = MailHelper.CreateSingleEmail(from, to, subject, content, null);
                var response = await client.SendEmailAsync(msg);
            }

        }
    }
}

