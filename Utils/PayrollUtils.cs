using ClockInTimeWeb.Data;
using iText.Commons.Actions.Contexts;
using Microsoft.EntityFrameworkCore;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace ClockInTimeWeb.Utils
{
    public static class PayrollUtils
    {

        public static int GetWorkedHoursForUser(int idFuncionario, int month, int year)
        {
            CitContext _context = new CitContext();

            int quantidadeRegistros = _context.Pontos
                .Count(p => p.Data.Month == month && p.Data.Year == year && p.IdFuncionario == idFuncionario);


            return quantidadeRegistros * 8;
        }

    }
}
