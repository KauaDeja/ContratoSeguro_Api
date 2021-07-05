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
    public class EsqueciMinhaSenhaCommandHandler : Notifiable, IHandlerCommand<EsqueciMinhaSenhaCommand>
    {
        //Chamamos nosso repositório
        private readonly IRecrutadoRepository _recrutadoRepository;
        private readonly IMailService _emailService;
        private readonly IUsuarioRepository _usuarioRepository;


        //Realizamos nossa injeção de dependência
        public EsqueciMinhaSenhaCommandHandler(IRecrutadoRepository recrutadoRepository, IMailService emailService, IUsuarioRepository usuarioRepository)
        {
            _recrutadoRepository = recrutadoRepository;
            _emailService = emailService;
            _usuarioRepository = usuarioRepository;

        }

        public ICommandResult Handle(EsqueciMinhaSenhaCommand command)
        {
            //Valida
            command.Validar();

            if (command.Invalid)
                return new GenericCommandResult(false, "Dados inválidos", command.Notifications);

            //Verifica se o email existe
            var usuario = _usuarioRepository.BuscarPorEmail(command.Email);

            if (usuario == null)
                return new GenericCommandResult(false, "Email inválido", null);

            //Gera nova senha
            string senha = Senha.GerarSenha();
            //Criptografa a nova senha
            usuario.AlterarSenha(Senha.Criptografar(senha));

            if (usuario.Invalid)
                return new GenericCommandResult(false, "Dados inválidos", usuario.Notifications);

            //Salva o usuario com nova senha no banco
            _usuarioRepository.Alterar(usuario);

            //TODO: Enviar email de SENHA NOVA
            //Enviar email de boas vindas
            _emailService.SendEmailAsync(usuario.Email, "Senha alterada", $"Sua Senha nova é: {senha}");

            return new GenericCommandResult(true, "Uma nova senha foi criada e enviada para o seu e-mail, verifique!!!", null);
        }
    }
}
