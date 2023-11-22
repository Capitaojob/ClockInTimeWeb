using System;
using System.Collections.Generic;

namespace ClockInTimeWeb.Data;

public partial class Bonificacao
{
    public int IdBon { get; set; }

    public decimal ValorBon { get; set; }

    public char Status { get; set; }

    public string Nome { get; set; } = null!;
}
