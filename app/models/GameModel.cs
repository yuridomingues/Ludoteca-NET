using Ludo.Enum;

namespace Ludo.Models
{
    public class GameModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public CategoryGame CategoryGame { get; set; }
        public bool Availability { get; set; } = true;
        public string Description { get; set; } = string.Empty;

        public GameModel(int id, string name, decimal value, CategoryGame categoryGame, string description)  // [AV1-2] - Construtor da classe
        {
            if (id <= 0)
                throw new ArgumentException("O Id deve ser maior que zero.", nameof(id));    // [AV1-5]
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("O nome do jogo não pode ser vazio.", nameof(name)); // [AV1-5]
            if (value < 0)
                throw new ArgumentException("O valor não pode ser negativo.", nameof(value)); // [AV1-5]

            Id = id;
            Name = name;
            Value = value;
            CategoryGame = categoryGame;
            Description = description;
        }
    }
}