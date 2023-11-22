using System;
using System.Collections.Generic;

namespace ClockInTimeWeb.Data;

public partial class Imposto
{
    public int IdInss { get; set; }

    public int IdIrrf { get; set; }

    public virtual ICollection<DadosImposto> DadosImpostos { get; set; } = new List<DadosImposto>();
}
