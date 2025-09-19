using System;
using System.Linq;
using System.Text.Json;
using Ludo.Models;
using Ludo.Services;
using Ludo.Enum;

class Program
{
    static void Main(string[] args)
    {
        var gameService = new GameService();

        while (true)
        {
            Console.WriteLine("\n=== Bem-vindo à Ludoteca! ===");
            Console.WriteLine("Escolha uma opção:");
            Console.WriteLine("1 - Cadastrar jogo");
            Console.WriteLine("2 - Listar jogos");
            Console.WriteLine("3 - Me cadastrar como membro");
            Console.WriteLine("4 - Consultar minhas multas");
            Console.WriteLine("5 - Consultar meus empréstimos");
            Console.WriteLine("6 - Fazer um empréstimo");
            Console.WriteLine("7 - Sair");
            Console.Write("Opção: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("Quantos jogos você quer cadastrar?");
                    if (!int.TryParse(Console.ReadLine(), out int totalGames))
                    {
                        Console.WriteLine("Número inválido!");
                        break;
                    }

                    for (int i = 0; i < totalGames; i++)
                    {
                        Console.WriteLine($"\nCadastro do jogo #{i + 1}");

                        GameModel game = new GameModel();

                        Console.Write("Nome: ");
                        game.Name = Console.ReadLine() ?? "Sem nome";

                        Console.Write("Valor: ");
                        if (!decimal.TryParse(Console.ReadLine(), out decimal valor))
                        {
                            Console.WriteLine("Valor inválido!");
                            continue;
                        }
                        game.Value = valor;

                        Console.Write("Categoria (Action, Adventure, RPG, Sports, Strategy, Simulation, Puzzle): ");
                        if (!Enum.TryParse(Console.ReadLine(), true, out CategoryGame category))
                        {
                            Console.WriteLine("Categoria inválida! Definindo como 'Action'.");
                            category = CategoryGame.Action;
                        }
                        game.CategoryGame = category;

                        Console.Write("Disponível? (true/false): ");
                        if (!bool.TryParse(Console.ReadLine(), out bool disponibilidade))
                        {
                            Console.WriteLine("Disponibilidade inválida! Definindo como 'true'.");
                            disponibilidade = true;
                        }
                        game.Availability = disponibilidade;

                        Console.Write("Descrição: ");
                        game.Description = Console.ReadLine() ?? "";

                        gameService.CreateGame(game);
                    }

                    Console.WriteLine("\nJogos cadastrados:");
                    Console.WriteLine(JsonSerializer.Serialize(gameService.GetAllGames(), new JsonSerializerOptions { WriteIndented = true }));
                    break;

                case "2":
                    Console.WriteLine("\nJogos cadastrados:");
                    Console.WriteLine(JsonSerializer.Serialize(gameService.GetAllGames(), new JsonSerializerOptions { WriteIndented = true }));
                    break;

                case "3":
                    Console.WriteLine("\n=== Cadastro de Membro ===");

                    Member member = new Member();

                    Console.Write("Nome: ");
                    member.Name = Console.ReadLine() ?? "Sem nome";

                    Console.Write("Email: ");
                    member.Email = Console.ReadLine() ?? "sememail@dominio.com";

                    Console.Write("Data de nascimento (dd/MM/yyyy): ");
                    if (!DateTime.TryParse(Console.ReadLine(), out DateTime birthDate))
                    {
                        Console.WriteLine("Data inválida! Definindo como 01/01/2000.");
                        birthDate = new DateTime(2000, 1, 1);
                    }
                    member.BirthDate = birthDate;

                    Console.Write("Telefone: ");
                    member.Phone = Console.ReadLine() ?? "";

                    member.Fine = 0;

                    member.Availability = true;

                    Member.RegisterMember(member);

                    Console.WriteLine("\nMembro cadastrado com sucesso!");
                    Console.WriteLine($"Id: {member.Id}, Nome: {member.Name}, Email: {member.Email}");
                    break;

                case "4":
                    Console.WriteLine("Funcionalidade de consulta de multas ainda não implementada.");
                    break;

                case "5":
                    Console.WriteLine("Insira seu id de membro");

                    if (!int.TryParse(Console.ReadLine(), out int memberId))
                    {
                        Console.WriteLine("Id inválido!");
                        break;
                    }

                    var emprestimos = EmprestimoService.ConsultarEmprestimos(memberId);

                    if (!emprestimos.Any())
                    {
                        Console.WriteLine("Nenhum empréstimo encontrado.");
                    }
                    else
                    {
                        Console.WriteLine("\nSeus empréstimos:");
                        foreach (var e in emprestimos)
                        {
                            Console.WriteLine($"- Jogo ID: {e.GameId}, Data de Devolução: {e.DataDevolucao:dd/MM/yyyy}");
                        }
                    }
                    break;

                case "6":
                    Console.WriteLine("Qual jogo quer pegar emprestado?");
                    Console.WriteLine("Jogos disponíveis:");
                    foreach (var g in gameService.GetAllGames())
                    {
                        Console.WriteLine($"- {g.Name}");
                    }

                    var gameToBorrow = Console.ReadLine();
                    var selectedGame = gameService.GetAllGames().FirstOrDefault(g => g.Name == gameToBorrow);

                    if (selectedGame == null)
                    {
                        Console.WriteLine("Jogo não encontrado.");
                        break;
                    }

                    Console.Write("Insira seu id de membro da ludoteca: ");
                    if (!int.TryParse(Console.ReadLine(), out memberId))
                    {
                        Console.WriteLine("Id inválido!");
                        break;
                    }

                    Console.Write("Quantos dias você deseja emprestar o jogo? ");
                    if (!int.TryParse(Console.ReadLine(), out int quantidadeDias))
                    {
                        Console.WriteLine("Número de dias inválido!");
                        break;
                    }

                    var registroDeEmprestimo = EmprestimoService.Emprestar(selectedGame.Id, memberId, quantidadeDias);
                    if (registroDeEmprestimo)
                        Console.WriteLine("Empréstimo realizado com sucesso!");
                    else
                        Console.WriteLine("Erro ao realizar o empréstimo. Jogo já emprestado.");
                    break;

                case "7":
                    Console.WriteLine("Saindo...");
                    return;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }
}
