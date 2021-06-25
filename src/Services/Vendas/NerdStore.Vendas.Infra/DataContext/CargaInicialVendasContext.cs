

using Microsoft.EntityFrameworkCore;
using NerdStore.Vendas.Domain.Entidades;
using NerdStore.Vendas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NerdStore.Vendas.Infra.DataContext
{
    public class CargaInicialVendasContext
    {
        public static void Carregar(VendasContext vendasContext)
        {

             vendasContext.Database.Migrate();
            if (!vendasContext.Vouchers.Any())
            {
                var vouchers = CargaVouchers();
                vendasContext.Vouchers.AddRange(vouchers);
                vendasContext.SaveChanges();
            }       
        }

        private static List<Voucher> CargaVouchers()
        {
            return new List<Voucher>
        {
            new Voucher("PROMO10",null,10,7,ETipoDescontoVoucher.Valor,DateTime.Now.AddMonths(1),true,false),
            new Voucher("5OFF",5,null,8,ETipoDescontoVoucher.Porcentagem,DateTime.Now.AddMonths(1),true,false) 
        };
    }
    }

 
}
