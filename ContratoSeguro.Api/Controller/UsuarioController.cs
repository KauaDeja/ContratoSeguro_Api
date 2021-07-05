using ContratoSeguro.Comum.Commands;
using ContratoSeguro.Comum.Utills;
using ContratoSeguro.Dominio.Commands.Usuarios;
using ContratoSeguro.Dominio.Handlers.Command.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace ContratoSeguro.Api.Controller
{
    [Route("v1/account/users")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        /// <summary>
        /// Atualiza a senha de um usuário
        /// </summary>
        /// <param name="command">Command de alterar senha do usuário</param>
        /// <param name="handler">Handler de alterar senha do usuário</param>
        /// <returns>Senha do usuário alterada</returns>
        [Route("update-password")]
        [Authorize]
        [HttpPut]
        public GenericCommandResult UpdatePassword(
           [FromBody] AlterarSenhaCommand command,
           [FromServices] AlterarSenhaCommandHandler handler
       )
        {
            var idUsuario = HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);
            command.IdUsuario = new Guid(idUsuario.Value);


            return (GenericCommandResult)handler.Handle(command);
        }

        /// <summary>
        /// Altera o nome do usuário
        /// </summary>
        /// <param name="command"></param>
        /// <param name="handler"></param>
        /// <returns>Retorna</returns>
        [Route("update-name")]
        [Authorize]
        [HttpPut]
        public GenericCommandResult UpdateName(
           [FromBody] AlterarNomeCommand command,
           [FromServices] AlterarNomeCommandHandler handler
       )
        {
            var idUsuario = HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);
            command.IdUsuario = new Guid(idUsuario.Value);


            return (GenericCommandResult)handler.Handle(command);
        }

        [HttpPut("image")]
        public GenericCommandResult UpdateImage([FromForm] AlterarImagemCommand command, [FromServices] AlterarImagemCommandHandler handle)
        {
            var idUsuario = HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);
            command.IdUsuario = new Guid(idUsuario.Value);

            return (GenericCommandResult)handle.Handle(command);
        }


        /// <summary>
        /// Esse metodo envia um email para o recrutado com as novas credenciais
        /// </summary>
        /// <param name="command">Command de Esqueci minha senha do recrutado</param>
        /// <param name="handler">Handler de Esqueci minha senha do recrutado</param>
        /// <returns></returns>
        [Route("reset-password")]
        [HttpPut]
        public GenericCommandResult ResetPasswordAllUsers(
            [FromBody] EsqueciMinhaSenhaCommand command,
            [FromServices] EsqueciMinhaSenhaCommandHandler handler
        )
        {
            var resultado = (GenericCommandResult)handler.Handle(command);

            return resultado;
        }


    }
}
