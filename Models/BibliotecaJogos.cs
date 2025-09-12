namespace Ludo.Models
{
    public class BibliotecaJogos
    {
        public List<Jogo> Jogos { get; set; } = new List<Jogo>();

        public void AdicionarJogo(Jogo jogo)
        {
            Jogos.Add(jogo);
            var jogosPath = "jogos.json";
            File.WriteAllText(jogosPath, System.Text.Json.JsonSerializer.Serialize(Jogos, new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));
        }

        public void RemoverJogo(Jogo jogo)
        {
            if (!Jogos.Remove(jogo))
            {
                throw new FileNotFoundException("Jogo não encontrado na lista.");
            }

            var jogosPath = "jogos.json";
            File.WriteAllText(jogosPath, System.Text.Json.JsonSerializer.Serialize(Jogos, new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));
        }

        public List<Jogo> ListarJogos()
        {
            var jogosPath = "jogos.json";
            if (File.Exists(jogosPath))
            {
            var json = File.ReadAllText(jogosPath);
            Jogos = System.Text.Json.JsonSerializer.Deserialize<List<Jogo>>(json) ?? new List<Jogo>();
            }
            return Jogos;
        }
    }
}
