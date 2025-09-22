using Ludo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ludo.Services
{
    public class LoanService
    {
        private readonly List<LoanModel> loans = new();
        private readonly GameService gameService;

        public LoanService(GameService gameService)
        {
            this.gameService = gameService;
        }

        public async Task<bool> BorrowAsync(int gameId, int memberId, int numberOfDays)
        {
            if (loans.Any(l => l.GameId == gameId && !l.ReturnDate.HasValue))
                return false;

            GameModel game = await gameService.GetGameById(gameId);

            if (game == null || !game.Availability)
                return false;

            GameModel loan = new LoanModel
            {
                Id = loans.Count + 1,
                GameId = gameId,
                MemberId = memberId,
                LoanDate = DateTime.Now,
                ExpectedReturnDate = DateTime.Now.AddDays(numberOfDays),
            };

            loans.Add(loan);

            await gameService.UpdateDisponibility(gameId, false);

            return true;
        }

        public IEnumerable<LoanModel> GetLoans(int memberId)
        {
            return loans.Where(l => l.MemberId == memberId);
        }

        public async Task<bool> ReturnLoanAsync(int loanId)
        {
            LoanModel loan = loans.FirstOrDefault(l => l.Id == loanId);

            if (loan == null || loan.ReturnDate.HasValue)
                return false;

            loan.ReturnDate = DateTime.Now;

            await gameService.UpdateDisponibility(loan.GameId, true);

            return true;
        }
    }
}
