using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ClockInTimeWeb.Data;

public partial class CitContext : DbContext
{
    public CitContext()
    {
    }

    public CitContext(DbContextOptions<CitContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bonificacao> Bonificacaos { get; set; }

    public virtual DbSet<Cargo> Cargos { get; set; }

    public virtual DbSet<DadosImposto> DadosImpostos { get; set; }

    public virtual DbSet<Endereco> Enderecos { get; set; }

    public virtual DbSet<Funcionario> Funcionarios { get; set; }

    public virtual DbSet<Imposto> Impostos { get; set; }

    public virtual DbSet<Ponto> Pontos { get; set; }

    public virtual DbSet<Telefone> Telefones { get; set; }

    public virtual DbSet<Time> Times { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONNECTIONSTRING"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bonificacao>(entity =>
        {
            entity.HasKey(e => e.IdBon).HasName("bonificacao_pkey");

            entity.ToTable("bonificacao");

            entity.Property(e => e.IdBon).HasColumnName("id_bon");
            entity.Property(e => e.Nome)
                .HasMaxLength(150)
                .HasColumnName("nome");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .HasColumnName("status");
            entity.Property(e => e.ValorBon)
                .HasPrecision(10, 2)
                .HasColumnName("valor_bon");
        });

        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.HasKey(e => e.IdCargo).HasName("cargo_pkey");

            entity.ToTable("cargo");

            entity.Property(e => e.IdCargo).HasColumnName("id_cargo");
            entity.Property(e => e.Administrador).HasColumnName("administrador");
            entity.Property(e => e.CargaHoraria).HasColumnName("carga_horaria");
            entity.Property(e => e.Departamento)
                .HasMaxLength(1)
                .HasColumnName("departamento");
            entity.Property(e => e.NomeCargo)
                .HasMaxLength(150)
                .HasColumnName("nome_cargo");
            entity.Property(e => e.Salario)
                .HasPrecision(10, 2)
                .HasColumnName("salario");
        });

        modelBuilder.Entity<DadosImposto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("dados_imposto_pkey");

            entity.ToTable("dados_imposto");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Ano).HasColumnName("ano");
            entity.Property(e => e.IdImposto).HasColumnName("id_imposto");
            entity.Property(e => e.Porcentagem)
                .HasPrecision(10, 2)
                .HasColumnName("porcentagem");
            entity.Property(e => e.SalarioMax)
                .HasPrecision(10, 2)
                .HasColumnName("salario_max");

            entity.HasOne(d => d.IdImpostoNavigation).WithMany(p => p.DadosImpostos)
                .HasForeignKey(d => d.IdImposto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_dados_imposto_impostos");
        });

        modelBuilder.Entity<Endereco>(entity =>
        {
            entity.HasKey(e => e.IdFuncEnd).HasName("enderecos_pkey");

            entity.ToTable("enderecos");

            entity.Property(e => e.IdFuncEnd)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_func_end");
            entity.Property(e => e.Bairro)
                .HasMaxLength(150)
                .HasColumnName("bairro");
            entity.Property(e => e.Cep)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("cep");
            entity.Property(e => e.Cidade)
                .HasMaxLength(150)
                .HasColumnName("cidade");
            entity.Property(e => e.Complemento)
                .HasMaxLength(150)
                .HasColumnName("complemento");
            entity.Property(e => e.Estado)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.Numero).HasColumnName("numero");
            entity.Property(e => e.Rua)
                .HasMaxLength(150)
                .HasColumnName("rua");

            entity.HasOne(d => d.IdFuncEndNavigation).WithOne(p => p.Endereco)
                .HasForeignKey<Endereco>(d => d.IdFuncEnd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_enderecos_funcionario");
        });

        modelBuilder.Entity<Funcionario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("funcionarios_pkey");

            entity.ToTable("funcionarios");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cargo).HasColumnName("cargo");
            entity.Property(e => e.Cpf)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("cpf");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.Nascimento).HasColumnName("nascimento");
            entity.Property(e => e.Nome)
                .HasMaxLength(150)
                .HasColumnName("nome");
            entity.Property(e => e.Senha)
                .HasMaxLength(150)
                .HasColumnName("senha");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<Imposto>(entity =>
        {
            entity.HasKey(e => e.IdInss).HasName("impostos_pkey");

            entity.ToTable("impostos");

            entity.Property(e => e.IdInss).HasColumnName("id_inss");
            entity.Property(e => e.IdIrrf).HasColumnName("id_irrf");
        });

        modelBuilder.Entity<Ponto>(entity =>
        {
            entity.HasKey(e => e.IdPonto).HasName("pontos_pkey");

            entity.ToTable("pontos");

            entity.Property(e => e.IdPonto).HasColumnName("id_ponto");
            entity.Property(e => e.Data).HasColumnName("data");
            entity.Property(e => e.Entrada)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("entrada");
            entity.Property(e => e.EntradaAl)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("entrada_al");
            entity.Property(e => e.IdFuncionario).HasColumnName("id_funcionario");
            entity.Property(e => e.Saida)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("saida");
            entity.Property(e => e.SaidaAl)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("saida_al");
        });

        modelBuilder.Entity<Telefone>(entity =>
        {
            entity.HasKey(e => e.IdFuncTel).HasName("telefones_pkey");

            entity.ToTable("telefones");

            entity.Property(e => e.IdFuncTel)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_func_tel");
            entity.Property(e => e.Tel)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("tel");

            entity.HasOne(d => d.IdFuncTelNavigation).WithOne(p => p.Telefone)
                .HasForeignKey<Telefone>(d => d.IdFuncTel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_telefones_funcionario");
        });

        modelBuilder.Entity<Time>(entity =>
        {
            entity.HasKey(e => e.IdTime).HasName("times_pkey");

            entity.ToTable("times");

            entity.Property(e => e.IdTime).HasColumnName("id_time");
            entity.Property(e => e.NomeTime)
                .HasMaxLength(150)
                .HasColumnName("nome_time");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
