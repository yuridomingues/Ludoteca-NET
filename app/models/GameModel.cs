namespace App.Models
{
    public class GameModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public CategoryGame CategoryGame { get; set; }
        public bool Availability { get; set; }
        public string Description { get; set; }
    }
}
