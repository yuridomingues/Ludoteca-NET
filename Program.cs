using System;
using System.Linq;
using System.Text.Json;
using Ludo.Models;
using Ludo.Services;
using Ludo.Enum;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        GameService gameService = new GameService();
        LoanService loanService = new LoanService(gameService);

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
            Console.WriteLine("7 - Devolver um empréstimo");
            Console.WriteLine("8 - Sair");
            Console.Write("Opção: ");

            string? option = Console.ReadLine();

            switch (option)
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

                        Console.Write("Nome: ");
                        string nome = Console.ReadLine() ?? "Sem nome";

                        Console.Write("Valor: ");
                        if (!decimal.TryParse(Console.ReadLine(), out decimal valor))
                        {
                            Console.WriteLine("Valor inválido! Definindo como 0.");
                            valor = 0;
                        }

                        Console.Write("Categoria (Action, Adventure, RPG, Sports, Strategy, Simulation, Puzzle): ");
                        if (!Enum.TryParse(Console.ReadLine(), true, out CategoryGame category))
                        {
                            Console.WriteLine("Categoria inválida! Definindo como 'Action'.");
                            category = CategoryGame.Action;
                        }

                        Console.Write("Disponível? (true/false): ");
                        if (!bool.TryParse(Console.ReadLine(), out bool disponibilidade))
                        {
                            Console.WriteLine("Disponibilidade inválida! Definindo como 'true'.");
                            disponibilidade = true;
                        }

                        Console.Write("Descrição: ");
                        string descricao = Console.ReadLine() ?? "";

                        GameModel game = new GameModel(i + 1, nome, valor, category, descricao)
                        {
                            Availability = disponibilidade
                        };

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

                    Console.Write("Nome: ");
                    string nomeM = Console.ReadLine() ?? "Sem nome";

                    Console.Write("Email: ");
                    string email = Console.ReadLine() ?? "sememail@dominio.com";

                    Console.Write("Data de nascimento (dd/MM/yyyy): ");
                    if (!DateTime.TryParse(Console.ReadLine(), out DateTime birthDate))
                    {
                        Console.WriteLine("Data inválida! Definindo como 01/01/2000.");
                        birthDate = new DateTime(2000, 1, 1);
                    }

                    Console.Write("Telefone: ");
                    string telefone = Console.ReadLine() ?? "";

                    MemberModel member = new MemberModel(nomeM, email, birthDate, telefone);

                    MemberModel.RegisterMember(member);

                    Console.WriteLine("\nMembro cadastrado com sucesso!");
                    Console.WriteLine($"Id: {member.Id}, Nome: {member.Name}, Email: {member.Email}");
                    break;

                case "4":
                    Console.Write("Insira seu id de membro: ");
                    if (!int.TryParse(Console.ReadLine(), out int memberIdForFine))
                    {
                        Console.WriteLine("Id inválido!");
                        break;
                    }

                    MemberModel? memberForFine = MemberModel.GetMemberById(memberIdForFine);
                    if (memberForFine == null)
                    {
                        Console.WriteLine("Membro não encontrado.");
                        break;
                    }

                    IEnumerable<LoanModel> memberLoans = loanService.GetLoans(memberIdForFine);

                    if (!memberLoans.Any())
                    {
                        Console.WriteLine("Nenhuma multa encontrada para este membro.");
                        break;
                    }

                    Console.WriteLine("\n=== Consulta de Multas ===");
                    foreach (LoanModel l in memberLoans)
                    {
                        LoanModel loan = new LoanModel(
                            l.Id,                
                            l.GameId,
                            l.MemberId,
                            l.LoanDate,
                            l.ExpectedReturnDate
                        );

                        loan.SetReturnDate(DateTime.Now);
                        loan.CalculateFine(5, memberForFine);
                    }

                    Console.WriteLine($"Multa total do membro {memberForFine.Name}: R$ {memberForFine.Fine}");
                    break;


                case "5":
                    Console.Write("Insira seu id de membro: ");
                    if (!int.TryParse(Console.ReadLine(), out int memberId))
                    {
                        Console.WriteLine("Id inválido!");
                        break;
                    }

                    System.Collections.Generic.List<LoanModel> loans = loanService.GetLoans(memberId).ToList();

                    if (!loans.Any())
                    {
                        Console.WriteLine("Nenhum empréstimo encontrado.");
                    }
                    else
                    {
                        Console.WriteLine("\nSeus empréstimos:");
                        foreach (LoanModel l in loans)
                        {
                            Console.WriteLine(
                                $"- Empréstimo ID: {l.Id}, Jogo ID: {l.GameId}, " +
                                $"Prevista: {l.ExpectedReturnDate:dd/MM/yyyy}" +
                                (l.ReturnDate.HasValue ? $", Devolvido em: {l.ReturnDate:dd/MM/yyyy}" : "")
                            );
                        }
                    }
                    break;

                case "6":
                    Console.WriteLine("Jogos disponíveis:");
                    System.Collections.Generic.List<GameModel> availableGames = gameService.GetAllGames().Where(g => g.Availability).ToList();

                    if (!availableGames.Any())
                    {
                        Console.WriteLine("Nenhum jogo disponível para empréstimo.");
                        break;
                    }

                    foreach (GameModel g in availableGames)
                    {
                        Console.WriteLine($"- {g.Id} | {g.Name}");
                    }

                    Console.Write("Digite o ID do jogo que deseja pegar emprestado: ");
                    if (!int.TryParse(Console.ReadLine(), out int gameId))
                    {
                        Console.WriteLine("Id de jogo inválido!");
                        break;
                    }

                    GameModel? selectedGame = availableGames.FirstOrDefault(g => g.Id == gameId);
                    if (selectedGame == null)
                    {
                        Console.WriteLine("Jogo não encontrado ou indisponível.");
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

                    bool emprestimoOk = await loanService.BorrowAsync(selectedGame.Id, memberId, quantidadeDias);
                    if (emprestimoOk)
                        Console.WriteLine("Empréstimo realizado com sucesso!");
                    else
                        Console.WriteLine("Erro: jogo já emprestado ou indisponível.");
                    break;

                case "7":
                    Console.Write("Digite o ID do empréstimo que deseja devolver: ");
                    if (!int.TryParse(Console.ReadLine(), out int loanId))
                    {
                        Console.WriteLine("Id inválido!");
                        break;
                    }

                    bool retornoOk = await loanService.ReturnLoanAsync(loanId);
                    if (retornoOk)
                        Console.WriteLine("Jogo devolvido com sucesso!");
                    else
                        Console.WriteLine("Erro: empréstimo não encontrado ou já devolvido.");
                    break;

                case "8":
                    Console.WriteLine("Saindo...");
                    return;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }
}
