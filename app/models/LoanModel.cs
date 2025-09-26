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

        public LoanModel(int id, int gameId, int memberId, DateTime loanDate, DateTime expectedReturnDate)  // [AV1-2] - Construtor da classe
        {
            if (gameId <= 0)
                throw new ArgumentException("O jogo deve ser válido.", nameof(gameId)); // [AV1-5]
            if (memberId <= 0)
                throw new ArgumentException("O membro deve ser válido.", nameof(memberId)); // [AV1-5]
            if (expectedReturnDate <= loanDate)
                throw new ArgumentException("A data de devolução deve ser posterior à data de empréstimo.", nameof(expectedReturnDate)); // [AV1-5]

            Id = id;
            GameId = gameId;
            MemberId = memberId;
            LoanDate = loanDate;
            ExpectedReturnDate = expectedReturnDate;
        }

        public void SetReturnDate(DateTime returnDate)
        {
            ReturnDate = returnDate;
        }

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
