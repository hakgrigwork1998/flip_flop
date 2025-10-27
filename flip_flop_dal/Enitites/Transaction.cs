using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace flip_flop_dal.Enitites
{
    public class Transaction
    {
        public int TransactionId { get; set; }

        public Guid GeneratedTransactionId { get; set; }

        public decimal BetAmount { get; set; }

        public decimal WinAmount { get; set; }

        public string Currency { get; set; }

        public DateTime TransactionDateTime { get; set; }

        public bool IsSucceeded { get; set; }

        public int GameId { get; set; }

        public int PlayerId { get; set; }

        public virtual Player Player { get; set; }
    }
}
