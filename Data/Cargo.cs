using System;
using System.Collections.Generic;

namespace ClockInTimeWeb.Data;

public partial class Cargo
{
    public int IdCargo { get; set; }

    public decimal? Salario { get; set; }

    public char? Dp { get; set; }

    public int? CargaHoraria { get; set; }

    public string NomeCargo { get; set; } = null!;
}
