using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContratoSeguro.Comum.Utills
{
    public static class Upload
    {
        public static string Local(IFormFile arquivo)
        {
            var nomeArquivo = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(arquivo.FileName);

            var caminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\upload", nomeArquivo);

            using var streamArquivo = new FileStream(caminhoArquivo, FileMode.Create);

            arquivo.CopyTo(streamArquivo);

            return "http://localhost:5001/upload/" + nomeArquivo;
        }

        public static string Imagem(IFormFile _file)
        {
            //Gera um nome unico para o arquivo concatenando com o tipo dele
            var _nomeArquivo = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(_file.FileName);

            var _caminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images\" + _nomeArquivo);

            // passa para nosso repositorio o arquivo para o alocar
            using var _streamImagem = new FileStream(_caminhoArquivo, FileMode.Create);

            //faz uma copia do arquivo inserido no nosso repositorio
            _file.CopyTo(_streamImagem);

            return $"http://localhost:5001/upload/{_nomeArquivo}";
        }


    }
}
