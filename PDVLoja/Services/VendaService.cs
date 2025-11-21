using PDVLoja.Data;
using PDVLoja.Models;
using PDVLoja.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDVLoja.Services
{
    public class VendaService
    {
        private readonly PdvContext _context;
        private readonly PagamentoIntegrator _pagIntegrator;

        public VendaService(PdvContext context, PagamentoIntegrator pagIntegrator)
        {
            _context = context;
            _pagIntegrator = pagIntegrator;
        }

        public Produto BuscarProduto(string codigo)
        {
            return _context.Produtos.FirstOrDefault(p => p.CodigoBarras == codigo);
        }

        public bool FinalizarVenda(Venda venda, FormaPagamento forma)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                venda.Total = venda.CalcularTotal();
                bool pagamentoOk = _pagIntegrator.ProcessarPagamento(venda.Total, forma);

                if (!pagamentoOk) return false;

                foreach (var item in venda.Itens)
                {
                    item.Produto.AtualizarEstoque(-item.Quantidade);
                    _context.Update(item.Produto);
                }

                _context.Vendas.Add(venda);
                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                transaction.Rollback();
                return false;
            }
        }
    }
}
