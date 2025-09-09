using Ludo.Models;

namespace Ludo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var biblioteca = new BibliotecaJogos();

            while (true)
            {
                Console.WriteLine("Bem vindo á Ludoteca!");
                Console.WriteLine("1. Listar Jogos");
                Console.WriteLine("2. Fazer cadastro como membro");
                Console.WriteLine("3. Solicitar um empréstimo");
                Console.WriteLine("4. Voltar");
                Console.WriteLine("Escolha uma opção:");

                var opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        biblioteca.ListarJogos();
                        break;
                    case "2":
                        
                        break;
                    case "3":
                        // Chamar método para listar jogos
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }

        public void InterfaceJogo()
        {
           
        }
    }
}