using ContratoSeguro.Comum.Commands;
using ContratoSeguro.Comum.Queries;
using ContratoSeguro.Dominio.Commands.Usuarios;
using ContratoSeguro.Dominio.Entidades;
using ContratoSeguro.Dominio.Handlers.Command.Usuario;
using ContratoSeguro.Dominio.Handlers.Queries;
using ContratoSeguro.Dominio.Handlers.Queries.Usuario;
using ContratoSeguro.Dominio.Queries;
using ContratoSeguro.Dominio.Queries.Usuario;
using Microsoft.AspNetCore.Authorization;
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
    [Route("v1/account/employee")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        /// <summary>
        /// Esse método cadastra um funcionario
        /// </summary>
        /// <param name="command">Command de cadastrar funcionario</param>
        /// <param name="handler">Handler de cadastrar funcionario</param>
        /// <returns>Retorna o cadastro do funcionario</returns>
        [Route("signup")]
        //[Authorize(Roles = "Empresa")]
        [HttpPost]
        //Aqui nós passamos como parametro os Command e Handler
        public GenericCommandResult SignupEmployeeUser(CriarContaFuncionarioCommand command,
        ////Definimos que o CriarContaHanlde é um serviço
        [FromServices] CriarContaFuncionarioCommandHandler handler)
        {

            return (GenericCommandResult)handler.Handle(command);
        }

        /// <summary>
        /// Esse método lista os usuários do tipo funcionario
        /// </summary>
        /// <param name="handle">Handler</param>
        /// <returns>Funcionarios cadastrados</returns>
        [Route("lister-employee")]
        //[Authorize(Roles = "Funcionario")]
        [HttpGet]
        //Aqui nós passamos como parametro os Command e Handler
        public GenericQueryResult GetEmployee([FromServices] ListarFuncionarioQueryHandler handle)
        {
            ListarFuncionarioQuery query = new ListarFuncionarioQuery();

            return (GenericQueryResult)handle.Handle(query);
        }

        [Route("profile-employee/{id}")]
        [HttpGet]
        public GenericQueryResult GetProfile(Guid id,
           [FromServices] ListarDadosFuncionarioQueryHandler handler
           )
        {
            ListarDadosFuncionarioQuery query = new ListarDadosFuncionarioQuery();

            if (id == Guid.Empty)
                return new GenericQueryResult(false, "Informe um id válido", null);
            query.IdFuncionario = id;

            return (GenericQueryResult)handler.Handle(query);
        }

        /// <summary>
        /// Essse método busca por um nome de recrutado
        /// </summary>
        /// <param name="handle">Handler</param>
        /// <returns>Retorna a lista de nomes encontrados</returns>
        [Route("search-employee/{nome}")]
        //[Authorize(Roles = "Funcionario")]
        [HttpGet]
        //Aqui nós passamos como parametro os Command e Handler
        public GenericQueryResult GetSearchEmployee(string nome, [FromServices] BuscarPorNomeFuncionarioQueryHandler handle)
        {
            BuscarNomeFuncionarioQuery query = new BuscarNomeFuncionarioQuery();

            var tipoUsuario = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

            query.Nome = nome;

            return (GenericQueryResult)handle.Handle(query);
        }

        /// <summary>
        /// Deleta um funcionario
        /// </summary>
        /// <param name="command"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        [HttpDelete("delete-employee")]
        //[Authorize]
        public GenericCommandResult DeleteEmployee(
                    [FromBody] DeletarFuncionarioCommand command,
                                    [FromServices] DeletarFuncionarioCommandHandler handler
      )
        {

            return (GenericCommandResult)handler.Handle(command);
        }

        /// <summary>
        /// Esse método loga o funcionario/recrutado no sistema
        /// </summary>
        /// <param name="command">Command de logar funcionario/recrutado</param>
        /// <param name="handler">Command de logar funcionario/recrutad</param>
        /// <returns>Retorna o token</returns>
        [Route("signin")]
        [HttpPost]
        public GenericCommandResult SignIn(LogarCommandRecrutado command, [FromServices] LogarFuncionarioCommandHandler handler)
        {
            var resultado = (GenericCommandResult)handler.Handle(command);

            if (resultado.Sucesso)
            {
                var token = GerarJSONWebToken((Funcionario)resultado.Data);

                return new GenericCommandResult(resultado.Sucesso, resultado.Mensagem, new { token = token });
            }

            return new GenericCommandResult(false, resultado.Mensagem, resultado.Data);

        }
        /// <summary>
        /// Esse método gera o JWT
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns>Token</returns>
        private string GerarJSONWebToken(Funcionario userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ChaveSecretaContratoSeguroApi"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Definimos nossas Claims (dados da sessão) para poderem ser capturadas
            // a qualquer momento enquanto o Token for ativo
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.FamilyName, userInfo.Nome),
                new Claim(JwtRegisteredClaimNames.FamilyName, userInfo.CPF),
                new Claim(JwtRegisteredClaimNames.FamilyName, userInfo.DataNascimento.ToString()),
                new Claim(JwtRegisteredClaimNames.FamilyName, userInfo.RG),
                new Claim(JwtRegisteredClaimNames.FamilyName, userInfo.Formação),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim(ClaimTypes.Role, userInfo.Tipo.ToString()),
                new Claim("role", userInfo.Tipo.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, userInfo.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, userInfo.IdUsuario.ToString()),


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

    }
}
