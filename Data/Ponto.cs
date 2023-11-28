using System;
using System.Collections.Generic;

namespace ClockInTimeWeb.Data;

public partial class Ponto
{
    public int IdPonto { get; set; }

    public int IdFuncionario { get; set; }

    public DateOnly Data { get; set; }

    public DateTime? Entrada { get; set; }

    public DateTime? SaidaAl { get; set; }

    public DateTime? EntradaAl { get; set; }

    public DateTime? Saida { get; set; }
}
