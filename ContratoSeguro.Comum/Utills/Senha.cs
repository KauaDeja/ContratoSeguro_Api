using System;
using System.Collections.Generic;
using System.Text;

namespace ContratoSeguro.Comum.Utills
{
    public static class Senha
    {
        public static string Criptografar(string senha)
        {
            // Mais tarde iremos colocar o cod de criptografia
            // Foi usado para a criptografia de senha a library BCrypt
            return BCrypt.Net.BCrypt.HashPassword(senha);
        }

        public static bool ValidarSenha(string senha, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(senha, hash);
        }

        public static string GerarSenha()
        {
            string caracteres = "fkwfwe89wjfweoih85987yjn";
            string senha = "";
            Random random = new Random();
            for (int f = 0; f < 8; f++)
            {
                senha = senha + caracteres.Substring(random.Next(0, caracteres.Length - 1), 1);
            }

            return senha;
        }
    }
}
