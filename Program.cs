using System;
using System.Text.Json;
using App.Models;
using App.Services;

class Program
{
    static void Main(string[] args)
    {
        var service = new GameService();

        Console.WriteLine("Quantos jogos você quer cadastrar?");
        if (!int.TryParse(Console.ReadLine(), out int totalGames))
        {
            Console.WriteLine("Número inválido!");
            return;
        }

        for (int i = 0; i < totalGames; i++)
        {
            Console.WriteLine($"\nCadastro do jogo #{i + 1}");

            var game = new GameModel();

            Console.Write("Nome: ");
            game.Name = Console.ReadLine()!;

            Console.Write("Valor: ");
            game.Value = decimal.Parse(Console.ReadLine()!);

            Console.WriteLine("Categoria (Action, Adventure, RPG, Sports, Strategy, Simulation, Puzzle): ");
            Enum.TryParse(Console.ReadLine(), out CategoryGame category);
            game.CategoryGame = category;

            Console.Write("Disponível? (true/false): ");
            game.Availability = bool.Parse(Console.ReadLine()!);

            Console.Write("Descrição: ");
            game.Description = Console.ReadLine()!;

            service.CreateGame(game);
        }

        
        string json = JsonSerializer.Serialize(service.GetAllGames(), new JsonSerializerOptions { WriteIndented = true });
        Console.WriteLine("\nJogos cadastrados:");
        Console.WriteLine(json);
    }
}
