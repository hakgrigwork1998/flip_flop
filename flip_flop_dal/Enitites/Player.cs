using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace flip_flop_dal.Enitites
{
    public class Player
    {
        public int PlayerId { get; set; }

        public string Username { get; set; }

        public long ExternalId { get; set; }

        public int PartnerId { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
