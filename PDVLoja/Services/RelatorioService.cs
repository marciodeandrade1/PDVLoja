using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using PDVLoja.Data;
using PDVLoja.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDVLoja.Services
{
    public class RelatorioService
    {
        private readonly PdvContext _context;

        public RelatorioService(PdvContext context)
        {
            _context = context;
        }

        public List<ItemRelatorio> GerarRelatorioItensMaisVendidos(DateTime inicio, DateTime fim, int topN = 10)
        {
            var query = _context.ItensVenda
                .Include(iv => iv.Produto)
                .Where(iv => iv.Venda.Data >= inicio && iv.Venda.Data <= fim)
                .GroupBy(iv => iv.Produto)
                .Select(g => new ItemRelatorio
                {
                    NomeProduto = g.Key.Nome,
                    TotalVendido = g.Sum(iv => iv.Quantidade),
                    EstoqueAtual = g.Key.QuantidadeEstoque,
                    StatusMinimo = g.Key.QuantidadeEstoque < g.Key.EstoqueMinimo ? "Baixo" : "OK"
                })
                .OrderByDescending(ir => ir.TotalVendido)
                .Take(topN)
                .ToList();

            return query;
        }

        public List<Produto> GerarRelatorioEstoqueMinimo()
        {
            return _context.Produtos.Where(p => p.QuantidadeEstoque < p.EstoqueMinimo).ToList();
        }

        public void ExportarPDF<T>(List<T> dados, string caminho) where T : class
        {
            // Implementação simplificada com iTextSharp
            using (var doc = new Document())
            {
                PdfWriter.GetInstance(doc, new FileStream(caminho, FileMode.Create));
                doc.Open();
                var table = new PdfPTable(4); // Ajustar colunas conforme T
                // Adicionar headers e dados via reflection ou manual
                doc.Add(table);
                doc.Close();
            }
        }

        public void ExportarExcel<T>(List<T> dados, string caminho) where T : class
        {
            using (var package = new ExcelPackage(new FileInfo(caminho)))
            {
                var ws = package.Workbook.Worksheets.Add("Relatorio");
                // Preencher com dados via EPPlus
                package.Save();
            }
        }
    }

    public class ItemRelatorio
    {
        public string NomeProduto { get; set; }
        public int TotalVendido { get; set; }
        public int EstoqueAtual { get; set; }
        public string StatusMinimo { get; set; }
    }
}
