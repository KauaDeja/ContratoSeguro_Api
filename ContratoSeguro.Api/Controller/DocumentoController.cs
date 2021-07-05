using ContratoSeguro.Comum.Commands;
using ContratoSeguro.Comum.Queries;
using ContratoSeguro.Comum.Utills;
using ContratoSeguro.Dominio.Commands.Documentos;
using ContratoSeguro.Dominio.Handlers.Command.Documento;
using ContratoSeguro.Dominio.Handlers.Queries.Documento;
using ContratoSeguro.Dominio.Queries.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace ContratoSeguro.Api.Controller
{
    [Route("v1/document")]
    [ApiController]
    public class DocumentoController : ControllerBase
    {
        [Route("upload")]
        //[Authorize(Roles = "Funcionario")]
        [HttpPost]
        public ICommandResult UploadFiles( IFormFile arquivo)
        ////Definimos que o CriarContaHanlde é um serviço
        {
            if (arquivo == null)
                return new GenericCommandResult(false, "Envie um arquivo!", null);

            var urlIdocumento = Upload.Local(arquivo);

            return new GenericCommandResult(true, "Upload concluído com sucesso!", urlIdocumento);
        }

        [Route("send/{id}")]
        [HttpPost]
        public GenericCommandResult SendFiles(
            [FromBody] EnviarArquivoCommand command,
            [FromServices] EnviarArquivoCommandHandler handler
        )
        {
            var idUsuario = HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);
            command.IdUsuario = new Guid(idUsuario.Value);

            return (GenericCommandResult)handler.Handle(command);
        }

        [HttpDelete]
        [Authorize]
        public GenericCommandResult DeleteDocument(
                    [FromBody] DeletarDocumentoCommand command,
                                    [FromServices] DeletarDocumentoCommandHandler handler
      )
        {

            return (GenericCommandResult)handler.Handle(command);
        }


        [Route("{id}")]
        [HttpGet]
        public GenericQueryResult ListDoc(Guid id,
            [FromServices] ListarDocumentosRecrutadoQueryHandler handler
            )
        {
            ListarDocumentosRecrutadoQuery query = new ListarDocumentosRecrutadoQuery();

            if (id == Guid.Empty)
                return new GenericQueryResult(false, "Informe um id válido", null);
            query.IdRecrutado = id;

            return (GenericQueryResult)handler.Handle(query);
        }
    }
}
