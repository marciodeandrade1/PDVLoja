using Microsoft.Extensions.DependencyInjection;
using PDVLoja.Data;

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

        private void btnLogin_Click_1(object sender, EventArgs e)
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