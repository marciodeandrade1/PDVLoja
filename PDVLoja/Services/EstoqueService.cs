using PDVLoja.Data;
using PDVLoja.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDVLoja.Services
{
    public class EstoqueService
    {
        private readonly PdvContext _context;

        public EstoqueService(PdvContext context)
        {
            _context = context;
        }

        public void AdicionarProduto(Produto produto)
        {
            _context.Produtos.Add(produto);
            _context.SaveChanges();
        }

        public List<Produto> GetProdutosBaixoEstoque()
        {
            return _context.Produtos.Where(p => p.QuantidadeEstoque < p.EstoqueMinimo).ToList();
        }

        // Outros métodos: Editar, Remover, etc.
    }
}
