using flip_flop_dal.Enitites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace flip_flop_dal.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(FlipFlopContext context) 
            : base(context)
        {
        }
    }
}
