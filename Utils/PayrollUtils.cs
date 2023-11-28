using ClockInTimeWeb.Data;

namespace ClockInTimeWeb.Utils
{
    public static class PayrollUtils
    {

        public static int GetWorkedHoursForUser(int idFuncionario, int month, int year)
        {
            DateTime inicioDoMes = new DateTime(year, month, 1);
            DateTime fimDoMes = inicioDoMes.AddMonths(1).AddDays(-1);
            
            CitContext _context = new CitContext();

            var horasPorDia = _context.Pontos
                .Where(r => r.IdFuncionario == idFuncionario && r.Data >= DateUtils.ParseDateTimeToDateOnly(inicioDoMes) && r.Data <= DateUtils.ParseDateTimeToDateOnly(fimDoMes))
                .Sum(r => ((r.Saida - r.Entrada - (r.EntradaAl - r.SaidaAl)) ?? TimeSpan.Zero).TotalHours);

            return (int)horasPorDia;

            //var totalHorasTrabalhadas = _context.Pontos
            //    .Where(r => r.IdFuncionario == idFuncionario && r.Data >= DateUtils.ParseDateTimeToDateOnly(inicioDoMes) && r.Data <= DateUtils.ParseDateTimeToDateOnly(fimDoMes))
            //    .Select(r => (r.Saida - r.Entrada - (r.EntradaAl - r.SaidaAl)) ?? TimeSpan.Zero)
            //    .ToList();

            //var totalHours = totalHorasTrabalhadas.Sum(ts => ts.TotalHours);

            //return (int)totalHours;
        }

    }
}
