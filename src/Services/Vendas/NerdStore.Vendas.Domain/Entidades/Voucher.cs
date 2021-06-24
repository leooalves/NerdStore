

using FluentValidation;
using NerdStore.Shared.Entidades;
using NerdStore.Vendas.Domain.Enums;
using System;
using System.Collections.Generic;

namespace NerdStore.Vendas.Domain.Entidades
{
    public class Voucher : Entity
    {
        protected Voucher() { }
        
        public Voucher(string codigo, decimal? percentual, decimal? valorDesconto, int quantidade, ETipoDescontoVoucher tipoDescontoVoucher, DateTime dataValidade, bool ativo, bool utilizado)
        {
            Codigo = codigo;
            Percentual = percentual;
            ValorDesconto = valorDesconto;
            Quantidade = quantidade;
            TipoDescontoVoucher = tipoDescontoVoucher;                       
            DataValidade = dataValidade;
            Ativo = ativo;
            Utilizado = utilizado;
            DataCadastro = DateTime.Now;
        }

        public string Codigo { get; private set; }
        public decimal? Percentual { get; private set; }
        public decimal? ValorDesconto { get; private set; }
        public int Quantidade { get; private set; }
        public ETipoDescontoVoucher TipoDescontoVoucher { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public DateTime? DataUtilizacao { get; private set; }
        public DateTime DataValidade { get; private set; }
        public bool Ativo { get; private set; }
        public bool Utilizado { get; private set; }

         //EF Rel.
        public ICollection<Pedido> Pedidos { get; set; }

        internal void ValidarSeAplicavel()
        {
            Validar(this, new VoucherAplicavelValidation());
        }

        internal void UtilizarVoucher()
        {            
            if(Quantidade == 0)
            {
                Utilizado = true;
                DataUtilizacao = DateTime.Now;
            }
            else
            {
                Quantidade -= 1;
            }            
        }
    }

    public class VoucherAplicavelValidation : AbstractValidator<Voucher>
    {

        public VoucherAplicavelValidation()
        {
            RuleFor(c => c.DataValidade)
                .Must(DataVencimentoSuperiorAtual)
                .WithMessage("Este voucher está expirado.");

            RuleFor(c => c.Ativo)
                .Equal(true)
                .WithMessage("Este voucher não é mais válido.");

            RuleFor(c => c.Utilizado)
                .Equal(false)
                .WithMessage("Este voucher já foi utilizado.");

            RuleFor(c => c.Quantidade)
                .GreaterThan(0)
                .WithMessage("Este voucher não está mais disponível");
        }

        protected static bool DataVencimentoSuperiorAtual(DateTime dataValidade)
        {
            return dataValidade >= DateTime.Now;
        }
    }
}
