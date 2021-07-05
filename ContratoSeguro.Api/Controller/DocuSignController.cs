using ContratoSeguro.Api.DocuSign;
using ContratoSeguro.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ContratoSeguro.Api.DocuSign.DocuSignClient;

namespace ContratoSeguro.Api.Controller
{
    [Route("v1/docusigns")]
    [ApiController]
    public class DocuSignController : ControllerBase
    {
        public readonly DocuSignCredentials _docuSignCredentials;

        public DocuSignController(DocuSignKeys docuSignKeys)
        {
            _docuSignCredentials = new DocuSignCredentials(docuSignKeys.Email, docuSignKeys.Password, docuSignKeys.ApiKey);
        }

        [HttpPost]
        public IActionResult Post(UserModel user)
        {
            var emailTemplate = new DocuSignEmailTemplate($"Contrato Clt {user.Nome}", "É um prazer ter você em nosso time, vamos entrar com o pé direito, assine este documento e estamos ansiosos para sua chegada ao elenco");

            var docuSignTemplate = new DocuSignTemplate("cd3dffcd-a1f3-496a-89c1-16028cf021a6", new List<string> { "Test Email Recipient" });

            var docuSignEnvelope = new DocuSignEnvelopes(_docuSignCredentials, emailTemplate, docuSignTemplate);

            var result = docuSignEnvelope.Create(user.Nome, user.Email);

            return Ok(result);
        }


        [HttpGet]
        public IActionResult Get()
        {
            var docuSignEnvelope = new DocuSignEnvelopes(_docuSignCredentials);

            var result = docuSignEnvelope.List();

            return Ok(result);
        }

        [HttpGet("{envelopeId}")]
        public IActionResult Get(string envelopeId)
        {
            var docuSignEnvelope = new DocuSignEnvelopes(_docuSignCredentials);

            var result = docuSignEnvelope.Get(envelopeId);

            return Ok(result);
        }

        [HttpGet("{envelopeId}/download")]
        public IActionResult Download(string envelopeId)
        {
            var docuSignEnvelope = new DocuSignEnvelopes(_docuSignCredentials);

            var stream = docuSignEnvelope.Download(envelopeId);

            return Ok();
        }
    }
}
