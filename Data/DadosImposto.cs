using System;
using System.Collections.Generic;

namespace ClockInTimeWeb.Data;

public partial class DadosImposto
{
    public int Id { get; set; }

    public int IdImposto { get; set; }

    public decimal Porcentagem { get; set; }

    public int Ano { get; set; }

    public decimal SalarioMax { get; set; }

    public virtual Imposto IdImpostoNavigation { get; set; } = null!;
}
