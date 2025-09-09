using Ludo.Models;

namespace Ludo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Exemplo para inicialização do Program.cs
            Jogo jogo = new Jogo
            {
                Id = 1,
                Nome = "Jogo da Vida",
                Preco = 99.99m,
                DataDeEntrada = DateTime.Now,
                Disponibilidade = true,
                Descricao = "Um jogo divertido para toda a família."
            };
        }
    }
}