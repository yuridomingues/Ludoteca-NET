using Ludo.Models;

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

        public void Borrow(int gameId, int memberId, int numberOfDays)
        {
            if (memberId <= 0)
            {
                throw new ArgumentNullException("Member cannot be null.");
            }

            LoanModel loan = new LoanModel
            {
                GameId = gameId,
                MemberId = memberId,
                LoanDate = DateTime.Now,
                ExpectedReturnDate = DateTime.Now.AddDays(numberOfDays)
            };

            var loanPath = "loans.json";
            File.WriteAllText(loanPath, System.Text.Json.JsonSerializer.Serialize(
                loan,
                new System.Text.Json.JsonSerializerOptions { WriteIndented = true }
            ));
        }
    }
}
