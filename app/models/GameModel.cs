namespace App.Models
{
    public class GameModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public CategoryGame CategoryGame { get; set; }
        public bool Availability { get; set; } = true;
        public string Description { get; set; } = string.Empty;
    }
}
