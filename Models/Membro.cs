namespace Ludo.Models;

public class Membro
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DataDeNascimento { get; set; }
    public int Telefone { get; set; }
    public bool Ativo { get; set; } = true;
}
