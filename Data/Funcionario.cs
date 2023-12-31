﻿using System;
using System.Collections.Generic;

namespace ClockInTimeWeb.Data;

public partial class Funcionario
{
    public int Id { get; set; }

    public DateOnly Nascimento { get; set; }

    public int Cargo { get; set; }

    public int Status { get; set; }

    public string? Senha { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Cpf { get; set; } = null!;

    public virtual Endereco? Endereco { get; set; }

    public virtual Telefone? Telefone { get; set; }
}
