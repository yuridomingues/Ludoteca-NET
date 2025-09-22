namespace Ludo.Services
{
    public static class LoanService
    {
        private static List<(int GameId, int MemberId, DateTime ReturnDate)> loans = new();

        public static bool Borrow(int gameId, int memberId, int numberOfDays)
        {
            if (loans.Any(l => l.GameId == gameId))
                return false;

            loans.Add((gameId, memberId, DateTime.Now.AddDays(numberOfDays)));
            return true;
        }

        public static IEnumerable<(int GameId, int MemberId, DateTime ReturnDate)> GetLoans(int memberId)
        {
            return loans.Where(l => l.MemberId == memberId);
        }
    }
}
