using flip_flop_dal.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace flip_flop_dal.Workers
{
    public interface IUnitOfWork : IDisposable
    {
        ITransactionRepository TransactionRepository { get; }
        IPlayerRepository PlayerRepository { get; }
        void Save();
    }
}
