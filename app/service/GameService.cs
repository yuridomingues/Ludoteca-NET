using App.Models;
using System.Collections.Generic;

namespace App.Services
{
    public class GameService
    {
        private List<GameModel> _games = new List<GameModel>();

        public void CreateGame(GameModel game)
        {
            _games.Add(game);
        }

        public void RemoveGame(int id)
        {
            _games.RemoveAll(g => g.Id == id);
        }

        public List<GameModel> GetAllGames()
        {
            return _games;
        }
    }
}
