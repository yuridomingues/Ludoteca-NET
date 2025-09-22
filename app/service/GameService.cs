using Ludo.Models;
using System.Collections.Generic;

namespace Ludo.Services
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

        public Task<GameModel> GetGameById(int id)
        {
            GameModel game = games.FirstOrDefault(g => g.Id == id);
            return Task.FromResult(game);
        }

        private async Task UpdateDisponibility(int id, bool newStatus)
        {
            GameModel game = await GetGameById(id);

            if (game != null)
            {
                game.Availability = newStatus;
            }
        }
    }

}
