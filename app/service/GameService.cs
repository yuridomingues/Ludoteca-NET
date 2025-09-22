using Ludo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ludo.Services
{
    public class GameService
    {
        private readonly List<GameModel> games = new();
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

        public Task<GameModel?> GetGameById(int id)
        {
            GameModel? game = games.FirstOrDefault(g => g.Id == id);
            return Task.FromResult(game);
        }

        public async Task UpdateDisponibility(int id, bool newStatus)
        {
            GameModel? game = await GetGameById(id);

            if (game != null)
            {
                game.Availability = newStatus;
            }
        }
    }
}
