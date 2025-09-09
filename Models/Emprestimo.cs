using Ludo.Models;

namespace Ludo.Models
{
    public class Emprestimo
    {
        public int Id { get; set; }
        public int JogoId { get; set; }
        public int MembroId { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DevolucaoPrevista { get; set; }
        public DateTime? DataDevolucao { get; set; }
        public decimal ValorMulta { get; set; }

        public void CalculoMulta(int valorPorDiasDeAtraso, Membro membro)
        {
            if (DataDevolucao.HasValue && DataDevolucao.Value > DevolucaoPrevista)
            {
                var diasAtraso = (DataDevolucao.Value - DevolucaoPrevista).Days;
                ValorMulta = diasAtraso * valorPorDiasDeAtraso;
                if (membro != null)
                {
                    membro.Multa += ValorMulta;
                }
            }
        }

        public void Emprestar(Jogo jogo, Membro membro, int quantidadeDias)
        {
            if (jogo == null || membro == null)
            {
                throw new ArgumentNullException("Jogo ou membro não podem ser nulos.");
            }

            Emprestimo = new Emprestimo
            {
                JogoId = jogo.Id,
                MembroId = membro.Id,
                DataEmprestimo = DateTime.Now,
                DevolucaoPrevista = DateTime.Now.AddDays(quantidadeDias)
            };

            var emprestimoPath = "emprestimos.json";
            File.WriteAllText(emprestimoPath, System.Text.Json.JsonSerializer.Serialize(Emprestimo, new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));
        }
    }
}