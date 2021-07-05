using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ContratoSeguro.Comum.Utills
{
    public class FileResult : IHttpActionResult
    {
        MemoryStream arquivoStuff;
        string nomeDoArquivo;
        HttpRequestMessage httpRequestMessage;
        HttpResponseMessage httpResponseMessage;

        public FileResult(MemoryStream data, HttpRequestMessage request, string filename)
        {
            arquivoStuff = data;
            httpRequestMessage = request;
            nomeDoArquivo = filename;
        }

        public System.Threading.Tasks.Task<HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
        {
            httpResponseMessage = httpRequestMessage.CreateResponse(HttpStatusCode.OK);
            httpResponseMessage.Content = new StreamContent(arquivoStuff);
            httpResponseMessage.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            httpResponseMessage.Content.Headers.ContentDisposition.FileName = nomeDoArquivo;
            httpResponseMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

            return System.Threading.Tasks.Task.FromResult(httpResponseMessage);
        }
    }
}
