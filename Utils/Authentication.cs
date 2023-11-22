using ClockInTimeWeb.Data;

namespace ClockInTimeWeb.Utils

{
    public static class Authentication
    {
        public static bool ValidateUser(string email, string password)
        {
            using (var dbContext = new CitContext())
            {
                // Verifica se existe um usuário com o email fornecido
                var user = dbContext.Funcionarios
                    .FirstOrDefault(u => u.Email == email);

                if (user != null)
                {
                    return Cryptography.CheckMatchingPasswords(password, email);
                }
            }

            return false;
        }
    }
}
