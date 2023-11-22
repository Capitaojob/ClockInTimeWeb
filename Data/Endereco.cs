using System;
using System.Collections.Generic;

namespace ClockInTimeWeb.Data;

public partial class Endereco
{
    public int IdFuncEnd { get; set; }

    public int Numero { get; set; }

    public string Rua { get; set; } = null!;

    public string Bairro { get; set; } = null!;

    public string? Complemento { get; set; }

    public string Cidade { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public string Cep { get; set; } = null!;

    public virtual Funcionario IdFuncEndNavigation { get; set; } = null!;
}
