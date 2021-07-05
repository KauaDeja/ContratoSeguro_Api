using ContratoSeguro.Comum.Commands;
using ContratoSeguro.Comum.Handlers;
using ContratoSeguro.Comum.Utills;
using ContratoSeguro.Dominio.Command.Usuarios;
using ContratoSeguro.Dominio.Entidades;
using ContratoSeguro.Dominio.Repositories;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Text;
using static ContratoSeguro.Comum.Utills.EnviarEmailUsuario;

namespace ContratoSeguro.Dominio.Handlers.Command.Usuario
{
    public class CriarContaRecrutadoCommandHandler : Notifiable, IHandlerCommand<CriarContaRecrutadoCommand>
    {
        private readonly IRecrutadoRepository _recrutadoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMailService _emailService;
        public CriarContaRecrutadoCommandHandler(IRecrutadoRepository recrutadoRepository, IMailService emailService, IUsuarioRepository usuarioRepository)
        {
            _recrutadoRepository = recrutadoRepository;
            _emailService = emailService;
            _usuarioRepository = usuarioRepository;
        }

        public ICommandResult Handle(CriarContaRecrutadoCommand command)
        {
            // Validar Command
            command.Validar();

            if (command.Invalid)
                return new GenericCommandResult(false, "Dados do usuário Inválidos", command.Notifications);

            //Verifica se email existe
            var cpfExiste = _recrutadoRepository.BuscarPorCPF(command.CPF);
            if (cpfExiste != null)
                return new GenericCommandResult(false, "CPF já cadastrado", null);

            //Verifica se email existe
            var usuarioExiste = _recrutadoRepository.BuscarPorEmail(command.Email);

            if (usuarioExiste != null)
                return new GenericCommandResult(false, "Email já cadastrado", null);

            string senha = command.Senha;
            string nome = command.Nome;
            string cpf = command.CPF;
            string email = command.Email;

            //Criptografar Senha 
            command.Senha = Senha.Criptografar(command.Senha);

            //Salvar Usuário
            var usuario = new Entidades.Recrutado(command.Nome, command.Email, command.Senha, command.CPF, command.Tipo);
            if (usuario.Invalid)
                return new GenericCommandResult(false, "Usuário Inválido", usuario.Notifications);

            _recrutadoRepository.Adicionar(usuario);
            //Enviar Email de Boas Vindas
            //Send Grid
            _emailService.SendEmailAsyncRecruted(usuario.Email, "Logue no sistema com as seguintes credenciais", $"Nome:{nome}\nEmail: {email}\nSenha: {senha}\nCPF: {cpf}");

            return new GenericCommandResult(true, "Usuário Criado! Verifique sua caixa de entrada para mais opções", null);
        }
    }
}
