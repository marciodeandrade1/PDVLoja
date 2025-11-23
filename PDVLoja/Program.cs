using PDVLoja.Data;
using PDVLoja.Forms;
using PDVLoja.Services;
using PDVLoja.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace PDVLoja
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            // Application.Run(new Form1());
            // Program.cs (.NET 8 WinForms com Host)
            // FIX: Remove usage of PDVLoja.CreateBuilder and args, as they do not exist.
            // Instead, manually configure services as needed.
            // Connection string centralizada
            string conn = @"Server=(localdb)\mssqllocaldb;Database=PDV_LojaDB;Trusted_Connection=True;";

            var services = new ServiceCollection();
            IServiceCollection servicesWithDbContext = services.AddDbContext<PdvContext>(opt => opt.UseSqlServer(conn));
            services.AddTransient<VendaService>();
            services.AddTransient<EstoqueService>();
            services.AddTransient<RelatorioService>();
            services.AddTransient<PagamentoIntegrator>();
            services.AddSingleton<DashboardViewModel>();
            services.AddTransient<FrmLogin>();

            var serviceProvider = services.BuildServiceProvider();

             Application.Run(serviceProvider.GetRequiredService<FrmLogin>());
        }
    }

}