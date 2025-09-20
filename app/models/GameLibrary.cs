using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ludo.Models
{
    public class GameLibrary
    {
        private readonly List<GameModel> _games = new List<GameModel>();

        public int Id { get; set; }
        public IReadOnlyCollection<GameModel> Games => _games.AsReadOnly();

        public void AddGame(GameModel game)
        {
            if (game != null)
            {
                _games.Add(game);
            }
        }

        public void RemoveGame(GameModel game)
        {
            if (game != null)
            {
                _games.Remove(game);
            }
        }
    }
}