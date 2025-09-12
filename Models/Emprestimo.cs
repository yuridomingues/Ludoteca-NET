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

        public void Emprestar(int jogoId, int membroId, int quantidadeDias)
        {
            if (membroId <= 0)
            {
                throw new ArgumentNullException("Membro não pode ser nulo.");
            }

            Emprestimo = new Emprestimo
            {
                JogoId = jogoId,
                MembroId = membroId,
                DataEmprestimo = DateTime.Now,
                DevolucaoPrevista = DateTime.Now.AddDays(quantidadeDias)
            };

            var emprestimoPath = "emprestimos.json";
            File.WriteAllText(emprestimoPath, System.Text.Json.JsonSerializer.Serialize(Emprestimo, new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));
        }
    }
}