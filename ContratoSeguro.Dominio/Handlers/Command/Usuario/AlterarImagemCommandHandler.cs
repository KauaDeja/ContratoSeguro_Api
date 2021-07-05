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

namespace ContratoSeguro.Dominio.Handlers.Command.Usuario
{
    public class AlterarImagemCommandHandler : Notifiable, IHandlerCommand<AlterarImagemCommand>
    {
        //Injetando o nosso repositório 
        private readonly IUsuarioRepository _usuarioRepository;


        //Injeção de dependência
        public AlterarImagemCommandHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        /// <summary>
        /// Método para validar os processos para alterar a senha de um usuário
        /// </summary>
        /// <param name="command">Comando de alterar senha</param>
        /// <returns>Nova senha</returns>
        public ICommandResult Handle(AlterarImagemCommand command)
        {
            string _imagemArquivo = null;
            //Verificando se a requisição não está inserindo um arquivo
            if (command.Arquivo != null)
            {
                //Caso um arquivo esteja sendo anexado, enviamos para o método de 'UploadFile'
                 _imagemArquivo = Upload.Imagem(command.Arquivo );

                //Atribuindo o caminho de exibição da imagem para o objeto
                //command.UrlFoto = _imagemArquivo;
            }
            

            //Fail Fast Validation
            //Aplicar as validações
            command.Validar();

            var usuarioexiste = _usuarioRepository.BuscarPorId(command.IdUsuario);
            if (usuarioexiste == null)
                return new GenericCommandResult(false, "Usuário não encontrado", command.Notifications);

            usuarioexiste.AlterarImagem(_imagemArquivo);

            if (usuarioexiste.Invalid)
                return new GenericCommandResult(false, "Dados inválidos", usuarioexiste.Notifications);

            //Salvar usuario banco
            _usuarioRepository.Alterar(usuarioexiste);

            //Enviar email de boas vindas

            return new GenericCommandResult(true, "Imagem alterada com sucesso!", null);
        }
    }
}
