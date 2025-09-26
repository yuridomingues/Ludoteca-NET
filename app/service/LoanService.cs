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

        public LoanService(GameService gameService) // [AV1-2]
        {
            this.gameService = gameService;
        }

        public async Task<bool> BorrowAsync(int gameId, int memberId, int numberOfDays)
        {
            try
            {
                if (loans.Any(l => l.GameId == gameId && !l.ReturnDate.HasValue))
                    return false;

                GameModel? game = await gameService.GetGameById(gameId);

                if (game == null || !game.Availability)
                    return false;

                LoanModel loan = new LoanModel(
                    loans.Count + 1,          
                    gameId,                    
                    memberId,                  
                    DateTime.Now,              
                    DateTime.Now.AddDays(numberOfDays) 
                );

                loans.Add(loan);

                await gameService.UpdateDisponibility(gameId, false);

                return true;
            }
            catch (Exception ex) // [AV1-5]
            {
                Console.WriteLine($"[ERRO - BorrowAsync] Não foi possível realizar o empréstimo: {ex.Message}");
                return false;
            }
        }

        public IEnumerable<LoanModel> GetLoans(int memberId)
        {
            try 
            {
                return loans.Where(l => l.MemberId == memberId);
            }
            catch (Exception ex) // [AV1-5]
            {
                Console.WriteLine($"[ERRO - GetLoans] Não foi possível recuperar os empréstimos: {ex.Message}");
                return Enumerable.Empty<LoanModel>();
            }
        }

        public async Task<bool> ReturnLoanAsync(int loanId)
        {
            try
            {
                LoanModel? loan = loans.FirstOrDefault(l => l.Id == loanId);

                if (loan == null || loan.ReturnDate.HasValue)
                    return false;

                loan.SetReturnDate(DateTime.Now);

                await gameService.UpdateDisponibility(loan.GameId, true);

                return true;
            }
            catch (Exception ex) // [AV1-5]
            {
                Console.WriteLine($"[ERRO - ReturnLoanAsync] Não foi possível devolver o empréstimo: {ex.Message}");
                return false;
            }
        }
    }
}
