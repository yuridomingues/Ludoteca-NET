namespace Ludo.Services
{
    public static class EmprestimoService
    {
        private static List<(int GameId, int MemberId, DateTime DataDevolucao)> emprestimos = new();

        public static bool Emprestar(int gameId, int memberId, int quantidadeDias)
        {
            if (emprestimos.Any(e => e.GameId == gameId))
                return false;

            emprestimos.Add((gameId, memberId, DateTime.Now.AddDays(quantidadeDias)));
            return true;
        }

        public static IEnumerable<(int GameId, int MemberId, DateTime DataDevolucao)> ConsultarEmprestimos(int memberId)
        {
            return emprestimos.Where(e => e.MemberId == memberId);
        }
    }
}
