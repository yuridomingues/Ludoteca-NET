using System;
using System.Collections.Generic;
using System.Linq;

namespace Ludo.Models
{
    public class LoanModel
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int MemberId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal FineAmount { get; set; }

        private static List<LoanModel> loans = new List<LoanModel>();

        public void CalculateFine(int finePerLateDay, MemberModel member)
        {
            if (ReturnDate.HasValue && ReturnDate.Value > ExpectedReturnDate)
            {
                int lateDays = (ReturnDate.Value - ExpectedReturnDate).Days;
                FineAmount = lateDays * finePerLateDay;
                if (member != null)
                {
                    member.Fine += FineAmount;
                }
            }
        }

        public static bool Borrow(int gameId, int memberId, int numberOfDays)
        {
            if (loans.Any(l => l.GameId == gameId && !l.ReturnDate.HasValue))
                return false; 

            loans.Add(new LoanModel
            {
                Id = loans.Count + 1,
                GameId = gameId,
                MemberId = memberId,
                LoanDate = DateTime.Now,
                ExpectedReturnDate = DateTime.Now.AddDays(numberOfDays)
            });

            return true;
        }

        public static IEnumerable<LoanModel> GetLoans(int memberId)
        {
            return loans.Where(l => l.MemberId == memberId);
        }

        public static void ReturnLoan(int loanId)
        {
            var loan = loans.FirstOrDefault(l => l.Id == loanId);
            if (loan != null)
            {
                loan.ReturnDate = DateTime.Now;
            }
        }
    }
}
