
using ContratoSeguro.Comum.Commands;
using ContratoSeguro.Comum.Handlers;
using ContratoSeguro.Comum.Utills;
using ContratoSeguro.Dominio.Commands.Usuarios;
using ContratoSeguro.Dominio.Entidades;
using ContratoSeguro.Dominio.Repositories;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ContratoSeguro.Comum.Utills.EnviarEmailUsuario;

namespace ContratoSeguro.Dominio.Handlers.Command.Usuario
{
    public class CriarContaEmpresaCommandHandler : Notifiable, IHandlerCommand<CriarContaEmpresaCommand>
    {
        private readonly IEmpresaRepository _empresaRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMailService _emailService;

        public CriarContaEmpresaCommandHandler(IEmpresaRepository empresaRepository, IMailService emailService, IUsuarioRepository usuarioRepository)
        {
            _empresaRepository = empresaRepository;
            _emailService = emailService;
            _usuarioRepository = usuarioRepository;

        }

        public ICommandResult Handle(CriarContaEmpresaCommand command)
        {
            // Validar Command
            command.Validar();

            if (command.Invalid)
                return new GenericCommandResult(false, "Dados do usuário Inválidos", command.Notifications);

            //Verifica se cpf existe
            var cnpjExiste = _empresaRepository.BuscarPorCNPJ(command.CNPJ);
            if (cnpjExiste != null)
                return new GenericCommandResult(false, "CNPJ já cadastrado", null);

            //Verifica se email existe
            var usuarioExiste = _empresaRepository.BuscarPorEmail(command.Email);

            if (usuarioExiste != null)
                return new GenericCommandResult(false, "Email já cadastrado", null);

            string senha = command.Senha;
            string nome = command.Nome;

            //Criptografar Senha 
            command.Senha = Senha.Criptografar(command.Senha);

            //Salvar Usuário
            var usuario = new Entidades.Empresa(command.Nome, command.Email, command.Senha,  command.CNPJ, command.RazaoSocial,command.Matriz, command.Logradouro, command.UF, command.Cidade, command.Numero, command.Bairro, command.DataAbertura, command.Tipo);

            if (usuario.Invalid)
                return new GenericCommandResult(false, "Usuário Inválido", usuario.Notifications);

            if (!string.IsNullOrEmpty(command.Telefone))
                usuario.AdicionarTelefone(command.Telefone);

            _empresaRepository.Adicionar(usuario);

            _emailService.SendEmailAsyncRecruted(usuario.Email, $"{nome}Senha Provisória: {senha}", null);

            return new GenericCommandResult(true, "Usuário Criado! Verifique sua caixa de entrada para mais opções", null);
        }
    }
}
