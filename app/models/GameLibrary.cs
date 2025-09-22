using System.Text.Json;

namespace Ludo.Models;

public class GameLibrary
{
    private readonly List<GameModel> _gameCollection = new List<GameModel>();
    private const string LibraryDataPath = "data/biblioteca.json";
    public IReadOnlyCollection<GameModel> Jogos => _gameCollection.AsReadOnly();

    public GameLibrary()
    {
        Directory.CreateDirectory("data");
    }
    public void AddGame(GameModel newGame)
    {
        if (newGame == null)
        {
            throw new ArgumentNullException(nameof(newGame), "Não é possível adicionar um jogo nulo.");
        }

        if (_gameCollection.Any(game => game.Id == newGame.Id))
        {
            throw new InvalidOperationException($"O jogo com ID {newGame.Id} já existe na biblioteca.");
        }
        
        _gameCollection.Add(newGame);
        Console.WriteLine($"Jogo '{newGame.Name}' adicionado com sucesso!");
    }

    public void SaveLibraryData()
    {
        // [AV1-3] Marcação para a serialização JSON
        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(_gameCollection, options);
        File.WriteAllText(LibraryDataPath, jsonString);
        Console.WriteLine("Dados da biblioteca salvos com sucesso!");
    }

    public void LoadLibraryData()
    {
        if (File.Exists(LibraryDataPath))
        {
            // [AV1-3] Marcação para a desserialização do JSON
            string jsonString = File.ReadAllText(LibraryDataPath);
            List<GameModel> loadedGames = JsonSerializer.Deserialize<List<GameModel>>(jsonString) ?? new List<GameModel>();
            if (loadedGames != null)
            {
                _gameCollection.Clear();
                _gameCollection.AddRange(loadedGames);
                Console.WriteLine("Dados da biblioteca carregados com sucesso!");
            }
        }
        else
        {
            Console.WriteLine("Arquivo de dados não encontrado. Começando com uma biblioteca vazia.");
        }
    }
}