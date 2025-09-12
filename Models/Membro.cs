using System.Text.Json;

namespace Ludo.Models
{
    public class Membro
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DataDeNascimento { get; set; }
        public string Telefone { get; set; } = string.Empty;
        public decimal Multa { get; set; } = 0;
        public bool Disponibilidade { get; set; } = true;

        public static List<Membro> Membros = new List<Membro>();
        private static string CaminhoArquivo = "membros.json";

        public static void CadastrarMembro(Membro membro)
        {
            membro.Id = Membros.Count + 1;
            Membros.Add(membro);
            SalvarMembros();
        }

        public static void ListarMembros()
        {
            foreach (var membro in Membros)
            {
                Console.WriteLine($"Id: {membro.Id}, Nome: {membro.Nome}, Email: {membro.Email}, Ativo: {membro.Ativo}");
            }
        }

        public static void SalvarMembros()
        {
            var json = JsonSerializer.Serialize(Membros, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(CaminhoArquivo, json);
        }

        public static void CarregarMembros()
        {
            if (File.Exists(CaminhoArquivo))
            {
                var json = File.ReadAllText(CaminhoArquivo);
                Membros = JsonSerializer.Deserialize<List<Membro>>(json) ?? new List<Membro>();
            }
        }
    }
}
