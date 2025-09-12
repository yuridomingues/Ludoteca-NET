using Ludo.Enums;

namespace Ludo.Models
{
    public class Jogo
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal Preco { get; private set; }
        public Categoria CategoriaJogo { get; set; }
        public DateTime DataDeEntrada { get; set; }
        public bool Disponibilidade { get; set; } = true;
        public string? Descricao { get; set; }
    }
}