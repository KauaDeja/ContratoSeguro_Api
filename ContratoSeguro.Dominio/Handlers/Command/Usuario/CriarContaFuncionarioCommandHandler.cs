using ContratoSeguro.Comum.Commands;
using ContratoSeguro.Comum.Handlers;
using ContratoSeguro.Comum.Utills;
using ContratoSeguro.Dominio.Commands.Usuarios;
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
     public class CriarContaFuncionarioCommandHandler : Notifiable, IHandlerCommand<CriarContaFuncionarioCommand>
    {
        private readonly IFuncionarioRepository _funcionarioRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMailService _emailService;
        public CriarContaFuncionarioCommandHandler(IFuncionarioRepository funcionarioRepository, IMailService emailService, IUsuarioRepository usuarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
            _emailService = emailService;
            _usuarioRepository = usuarioRepository;
        }

        public ICommandResult Handle(CriarContaFuncionarioCommand command)
        {
            // Validar Command
            command.Validar();

            if (command.Invalid)
                return new GenericCommandResult(false, "Dados do usuário Inválidos", command.Notifications);

            //Verifica se cpf existe
            var cpfExiste = _funcionarioRepository.BuscarPorCPF(command.CPF);
            if (cpfExiste != null)
                return new GenericCommandResult(false, "CPF já cadastrado", null);

            //Verifica se email existe
            var usuarioExiste = _funcionarioRepository.BuscarPorEmail(command.Email);

            if (usuarioExiste != null)
                return new GenericCommandResult(false, "Email já cadastrado", null);

            string senha = command.Senha;

            string nome = command.Nome;

            string cpf = command.CPF;

            string email = command.Email;

            //Criptografar Senha 
            command.Senha = Senha.Criptografar(command.Senha);

            //Salvar Usuário
            var usuario = new Entidades.Funcionario(command.Nome, command.Email, command.Senha,   command.CPF, command.RG, command.Formação, command.DataNascimento, command.Tipo);

            if (usuario.Invalid)
                return new GenericCommandResult(false, "Usuário Inválido", usuario.Notifications);

            _funcionarioRepository.Adicionar(usuario);

            //Enviar Email de Boas Vindas
            //Send Grid
            _emailService.SendEmailAsyncEmployee(usuario.Email, "Logue no sistema com as seguintes credenciais", $"Nome:{nome}\nEmail: {email}\nSenha: {senha}\nCPF: {cpf}");


            return new GenericCommandResult(true, "Usuário Criado! Verifique sua caixa de entrada para mais opções", null);
        }
    }
}
