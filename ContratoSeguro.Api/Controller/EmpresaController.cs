using ContratoSeguro.Comum.Commands;
using ContratoSeguro.Comum.Queries;
using ContratoSeguro.Dominio.Commands.Usuarios;
using ContratoSeguro.Dominio.Entidades;
using ContratoSeguro.Dominio.Handlers.Command.Usuario;
using ContratoSeguro.Dominio.Handlers.Queries;
using ContratoSeguro.Dominio.Handlers.Queries.Usuario;
using ContratoSeguro.Dominio.Queries;
using ContratoSeguro.Dominio.Queries.Usuario;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ContratoSeguro.Api.Controller
{
    [Route("v1/account/company")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        /// <summary>
        /// Esse método cadastra uma empresa
        /// </summary>
        /// <param name="command">Command de cadastrar empresa</param>
        /// <param name="handler">Handler de cadastrar empresa</param>
        /// <returns>Cadastro da empresa</returns>
        [Route("signup")]
        [HttpPost]
        public GenericCommandResult SignupCompanyUser(CriarContaEmpresaCommand command,
        ////Definimos que o CriarContaHanlde é um serviço
        [FromServices] CriarContaEmpresaCommandHandler handler)
        {

            return (GenericCommandResult)handler.Handle(command);
        }


        /// <summary>
        /// Esse método loga a empresa no no sistema
        /// </summary>
        /// <param name="command">Command de logar empresa</param>
        /// <param name="handler">Command de logar empresa</param>
        /// <returns>Retorna o token</returns>
        [Route("signin")]
        [HttpPost]
        public GenericCommandResult SignIn(LogarCommandEmpresa command, [FromServices] LogarEmpresaCommandHandler handler)
        {
            var resultado = (GenericCommandResult)handler.Handle(command);

            if (resultado.Sucesso)
            {
                var token = GerarJSONWebToken((Empresa)resultado.Data);

                return new GenericCommandResult(resultado.Sucesso, resultado.Mensagem, new { token = token });
            }

            return new GenericCommandResult(false, resultado.Mensagem, resultado.Data);

        }

        /// <summary>
        /// Gera o JWT
        /// </summary>
        /// <param name="userInfo">Objeto</param>
        /// <returns>Retorna o token</returns>
        private string GerarJSONWebToken(Empresa userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ChaveSecretaContratoSeguroApi"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Definimos nossas Claims (dados da sessão) para poderem ser capturadas
            // a qualquer momento enquanto o Token for ativo
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.FamilyName, userInfo.Nome),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim(ClaimTypes.Role, userInfo.Tipo.ToString()),
                new Claim("role", userInfo.Tipo.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, userInfo.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, userInfo.IdUsuario.ToString()),
                new Claim(JwtRegisteredClaimNames.FamilyName, userInfo.CNPJ.ToString()),
                new Claim(JwtRegisteredClaimNames.FamilyName, userInfo.Bairro),
                new Claim(JwtRegisteredClaimNames.FamilyName, userInfo.RazaoSocial),
                new Claim(JwtRegisteredClaimNames.FamilyName, userInfo.Matriz),
                new Claim(JwtRegisteredClaimNames.FamilyName, userInfo.Logradouro),
                new Claim(JwtRegisteredClaimNames.FamilyName, userInfo.UF),
                new Claim(JwtRegisteredClaimNames.FamilyName, userInfo.Cidade),
                new Claim(JwtRegisteredClaimNames.FamilyName, userInfo.Numero),
                new Claim(JwtRegisteredClaimNames.FamilyName, userInfo.DataAbertura.ToString())

            };

            // Configuramos nosso Token e seu tempo de vida
            var token = new JwtSecurityToken
                (
                    "contratoseguro",
                    "contratoseguro",
                    claims,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        /// <summary>
        /// Lista os dados da empresa
        /// </summary>
        /// <param name="id"></param>
        /// <param name="handler"></param>
        /// <returns>Retorna os dados do perfil</returns>
        [Route("profile-company/{id}")]
        [HttpGet]
        public GenericQueryResult GetProfile(Guid id,
            [FromServices] ListarDadosEmpresaQueryHandler handler
            )
        {
            ListarDadosEmpresaQuery query = new ListarDadosEmpresaQuery();

            if (id == Guid.Empty)
                return new GenericQueryResult(false, "Informe um id válido", null);
            query.IdEmpresa = id;

            return (GenericQueryResult)handler.Handle(query);
        }
    }
}
