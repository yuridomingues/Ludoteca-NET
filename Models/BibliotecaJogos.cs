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
            Jogos.Remove(jogo);
        }

        public void Emprestar(Jogo jogo, Membro membro, int quantidadeDias)
        {
            if (Jogos.Contains(jogo) && membro != null && membro.Ativo)
            {
                var emprestimo = new Emprestimo
                {
                    JogoId = jogo.Id,
                    MembroId = membro.Id,
                    DataEmprestimo = DateTime.Now,
                    DevolucaoPrevista = DateTime.Now.AddDays(quantidadeDias)
                };

                var emprestimos = new List<Emprestimo>();
                var emprestimosPath = "emprestimos.json";
                if (File.Exists(emprestimosPath))
                {
                    var json = File.ReadAllText(emprestimosPath);
                    emprestimos = System.Text.Json.JsonSerializer.Deserialize<List<Emprestimo>>(json) ?? new List<Emprestimo>();
                }
                emprestimos.Add(emprestimo);
                File.WriteAllText(emprestimosPath, System.Text.Json.JsonSerializer.Serialize(emprestimos, new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));
            }
        }
    }
}