using System;
using System.Collections.Generic;

namespace ClockInTimeWeb.Data;

public partial class Telefone
{
    public int IdFuncTel { get; set; }

    public string Tel { get; set; } = null!;

    public virtual Funcionario IdFuncTelNavigation { get; set; } = null!;
}
