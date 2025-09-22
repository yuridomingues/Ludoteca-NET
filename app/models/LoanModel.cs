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
    }
}
