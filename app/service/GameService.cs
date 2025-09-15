using App.Models;
using System.Collections.Generic;

namespace App.Services
{
    public class GameService
    {
        private List<GameModel> games = new List<GameModel>();
        private int nextId = 1; 

        public void CreateGame(GameModel game)
        {
            game.Id = nextId++; 
            games.Add(game);
        }

        public List<GameModel> GetAllGames()
        {
            return games;
        }
    }

}
