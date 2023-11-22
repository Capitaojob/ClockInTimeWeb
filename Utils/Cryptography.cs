using ClockInTimeWeb.Data;
using System;
using System.Security.Cryptography;
using System.Text;

namespace ClockInTimeWeb.Utils

{
    public static class Cryptography
    {
        public static String EncryptPassword(String password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Converter o array de bytes para uma string hexadecimal
                string hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                return hashedPassword;
            }
        }

        public static bool CheckMatchingPasswords(string inputedPassword, string UserEmailToCheck)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Hash a senha fornecida
                byte[] inputPasswordBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(inputedPassword));
                string hashedInputPassword = BitConverter.ToString(inputPasswordBytes).Replace("-", "").ToLower();

                var funcionario = new Funcionario();
                using (var dbContext = new CitContext())
                {
                    var user = dbContext.Funcionarios
                        .FirstOrDefault(u => u.Email == UserEmailToCheck);

                    if (user != null)
                    {
                        funcionario = user;
                    }
                }

                // Comparar a senha fornecida com a senha armazenada no objeto Funcionario
                return string.Equals(hashedInputPassword, funcionario.Senha, StringComparison.OrdinalIgnoreCase);
            }
        }
    }
}
