namespace PDVLoja.Models
{
    public class UsuarioCaixa
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string SenhaHash { get; set; }

        public bool Autenticar(string senha)
        {
            return BCrypt.Net.BCrypt.Verify(senha, SenhaHash);
        }

        public void SetSenha(string senha)
        {
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(senha);
        }
    }
}
