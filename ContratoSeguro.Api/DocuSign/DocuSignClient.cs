using ContratoSeguro.Api.Models;
using DocuSign.eSign.Api;
using DocuSign.eSign.Client;
using DocuSign.eSign.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ContratoSeguro.Api.DocuSign
{
    public class DocuSignClient
    {
        public DocuSignClient(DocuSignCredentials credentials)
        {
            // initialize client for desired environment (for production change to www)
            var apiClient = new ApiClient("https://demo.docusign.net/restapi");

            // configure 'X-DocuSign-Authentication' header
            var authHeader = "{\"Username\":\"" + credentials.Username + "\", \"Password\":\"" + credentials.Password + "\", \"IntegratorKey\":\"" + credentials.IntegratorKey + "\"}";

            Configuration.Default.AddDefaultHeader("X-DocuSign-Authentication", authHeader);

            // login call is available in the authentication api 
            var authApi = new AuthenticationApi();
            var loginInfo = authApi.Login();

            // parse the first account ID that is returned (user might belong to multiple accounts)
            this.AccountId = loginInfo.LoginAccounts[0].AccountId;
        }
        public string AccountId { get; set; }

        public class DocuSignEnvelopes
        {
            public DocuSignCredentials DocuSignCredentials { get; set; }

            public DocuSignEmailTemplate EmailTemplate { get; set; }

            public DocuSignTemplate DocuSignTemplate { get; set; }

            public DocuSignEnvelopes(DocuSignCredentials credentials)
            {
                this.DocuSignCredentials = credentials;
            }

            public DocuSignEnvelopes(DocuSignCredentials credentials, DocuSignEmailTemplate emailTemplate, DocuSignTemplate docuSignTemplate)
            {
                this.DocuSignCredentials = credentials;
                this.EmailTemplate = emailTemplate;
                this.DocuSignTemplate = docuSignTemplate;
            }

            public EnvelopeSummary Create(string name, string email)
            {
                var docuSignClient = new DocuSignClient(this.DocuSignCredentials);
                var accountId = docuSignClient.AccountId;

                // assign recipient to template role by setting name, email, and role name.  Note that the
                // template role name must match the placeholder role name saved in your account template.  
                var templateRoles = this.DocuSignTemplate.TemplateRoleNames.Select(m => new TemplateRole
                {
                    Email = email,
                    Name = name,
                    RoleName = m
                }).ToList();

                // create a new envelope which we will use to send the signature request
                var envelope = new EnvelopeDefinition
                {
                    EmailSubject = this.EmailTemplate.Subject,
                    EmailBlurb = this.EmailTemplate.MessageBody,
                    TemplateId = this.DocuSignTemplate.TemplateId,
                    TemplateRoles = templateRoles,
                    Status = "sent"
                };

                // |EnvelopesApi| contains methods related to creating and sending Envelopes (aka signature requests)
                var envelopesApi = new EnvelopesApi();
                var envelopeSummary = envelopesApi.CreateEnvelope(accountId, envelope);

                return envelopeSummary;
            }


            public IEnumerable<EnvelopeModel> List()
            {
                var docuSignClient = new DocuSignClient(this.DocuSignCredentials);
                var accountId = docuSignClient.AccountId;

                EnvelopesApi envelopesApi = new EnvelopesApi();
                var options = new EnvelopesApi.ListStatusChangesOptions();
                var date = DateTime.Now.AddDays(-60);
                options.fromDate = date.ToString("yyyy/MM/dd");
                var envelopesList = envelopesApi.ListStatusChanges(accountId, options);

                return envelopesList.Envelopes.Select(env => new EnvelopeModel
                {
                    CompletedDateTime = Convert.ToDateTime(env.CompletedDateTime),
                    CreatedDateTime = Convert.ToDateTime(env.CreatedDateTime),
                    EmailSubject = env.EmailSubject,
                    EnvelopeId = new Guid(env.EnvelopeId),
                    SentDateTime = Convert.ToDateTime(env.SentDateTime),
                    Status = env.Status
                });
            }

            public EnvelopeModel Get(string envelopeId)
            {
                var docuSignClient = new DocuSignClient(this.DocuSignCredentials);
                var accountId = docuSignClient.AccountId;

                EnvelopesApi envelopesApi = new EnvelopesApi();
                var env = envelopesApi.GetEnvelope(accountId, envelopeId);

                return new EnvelopeModel
                {
                    CompletedDateTime = Convert.ToDateTime(env.CompletedDateTime),
                    CreatedDateTime = Convert.ToDateTime(env.CreatedDateTime),
                    EmailSubject = env.EmailSubject,
                    EnvelopeId = new Guid(env.EnvelopeId),
                    SentDateTime = Convert.ToDateTime(env.SentDateTime),
                    Status = env.Status
                };
            }

            public MemoryStream Download(string envelopeId)
            {
                var docuSignClient = new DocuSignClient(this.DocuSignCredentials);
                var accountId = docuSignClient.AccountId;

                EnvelopesApi envelopesApi = new EnvelopesApi();
                var docsList = envelopesApi.ListDocuments(accountId, envelopeId);

                // GetDocument() API call returns a MemoryStream
                MemoryStream docStream = (MemoryStream)envelopesApi.GetDocument(accountId, envelopeId, docsList.EnvelopeDocuments[0].DocumentId);
                return docStream;
            }



        }
    }
}