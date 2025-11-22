using Microsoft.Extensions.DependencyInjection;
using PDVLoja.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDVLoja.Forms
{
    public partial class FrmLogin : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly PdvContext _context;

        public FrmLogin(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _context = _serviceProvider.GetService<PdvContext>();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var user = _context.Usuarios.FirstOrDefault(u => u.Nome == txtNome.Text);
            if (user != null && user.Autenticar(txtSenha.Text))
            {
                var frmMenu = _serviceProvider.GetService<FrmMenuPrincipal>();
                frmMenu.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Credenciais inválidas!");
            }
        }
    }
}
