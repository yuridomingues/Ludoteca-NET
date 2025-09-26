using System.Text.Json;

namespace Ludo.Models
{
    public class GameLibrary
    {
        private readonly List<GameModel> _gameCollection = new List<GameModel>();
        private const string LibraryDataPath = "data/biblioteca.json";

        public IReadOnlyCollection<GameModel> Jogos => _gameCollection.AsReadOnly();

        public GameLibrary() // [AV1-2] - Construtor da biblioteca de jogos
        {
            try // [AV1-5]
            {
                Directory.CreateDirectory("data"); 
            }
            catch (Exception ex)
            {
                throw new IOException("Erro ao criar o diretório de dados da biblioteca.", ex);
            }
        }

        public void AddGame(GameModel newGame)
        {
            if (newGame == null) // [AV1-5]
                throw new ArgumentNullException(nameof(newGame), "Não é possível adicionar um jogo nulo.");

            if (_gameCollection.Any(game => game.Id == newGame.Id)) // [AV1-5]
                throw new InvalidOperationException($"O jogo com ID {newGame.Id} já existe na biblioteca.");

            if (string.IsNullOrWhiteSpace(newGame.Name)) // [AV1-5]
                throw new ArgumentException("O jogo deve ter um nome válido.", nameof(newGame));

            if (newGame.Value < 0) // [AV1-5]
                throw new ArgumentException("O valor do jogo não pode ser negativo.", nameof(newGame));

            _gameCollection.Add(newGame);
            Console.WriteLine($"Jogo '{newGame.Name}' adicionado com sucesso!");
        }

        public void RemoveGame(int id)
        {
            var game = _gameCollection.FirstOrDefault(g => g.Id == id);
            if (game == null)
                throw new KeyNotFoundException($"Nenhum jogo com ID {id} foi encontrado.");

            _gameCollection.Remove(game);
            Console.WriteLine($"Jogo '{game.Name}' removido com sucesso!");
        }

        public GameModel GetGameById(int id)
        {
            var game = _gameCollection.FirstOrDefault(g => g.Id == id);
            if (game == null)
                throw new KeyNotFoundException($"Nenhum jogo com ID {id} foi encontrado.");
            return game;
        }

        public void SaveLibraryData()  
        // [AV1-3] Marcação para a serialização JSON
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(_gameCollection, options);
                File.WriteAllText(LibraryDataPath, jsonString);
                Console.WriteLine("Dados da biblioteca salvos com sucesso!");
            }
            catch (Exception ex) // [AV1-5]
            {
                throw new IOException("Erro ao salvar os dados da biblioteca.", ex);
            }
        }

        public void LoadLibraryData()
        // [AV1-3] Marcação para a serialização JSON
        {
            try
            {
                if (File.Exists(LibraryDataPath))
                {
                    string jsonString = File.ReadAllText(LibraryDataPath);
                    List<GameModel>? loadedGames = JsonSerializer.Deserialize<List<GameModel>>(jsonString);

                    if (loadedGames == null)
                        throw new InvalidOperationException("Erro ao desserializar os dados da biblioteca.");

                    _gameCollection.Clear();
                    _gameCollection.AddRange(loadedGames);
                    Console.WriteLine("Dados da biblioteca carregados com sucesso!");
                }
                else
                {
                    Console.WriteLine("Arquivo de dados não encontrado. Começando com uma biblioteca vazia.");
                }
            }
            catch (Exception ex) // [AV1-5]
            {
                throw new IOException("Erro ao carregar os dados da biblioteca.", ex);
            }
        }
    }
}
