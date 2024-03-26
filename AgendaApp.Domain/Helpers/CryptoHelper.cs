using System;
using System.Security.Cryptography;
using System.Text;

namespace AgendaApp.Domain.Helpers
{
    public static class CryptoHelper
    {
        /// <summary>
        /// Gera um hash SHA-256 de uma string de entrada.
        /// </summary>
        /// <param name="input">A string de entrada para a qual o hash SHA-256 será gerado.</param>
        /// <returns>Uma string representando o valor hash SHA-256 da entrada.</returns>
        public static string GenerateSHA256Hash(string input)
        {
            // Use SHA256.Create para instanciar um objeto SHA256
            using (SHA256 sha256 = SHA256.Create())
            {
                // Converte a string de entrada para um array de bytes e calcula o hash
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Constrói uma nova string StringBuilder
                StringBuilder builder = new StringBuilder();

                // Loop através de cada byte do array de bytes hash e formata cada um como uma string hexadecimal
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                // Retorna a string em formato hexadecimal
                return builder.ToString();
            }
        }
    }
}



