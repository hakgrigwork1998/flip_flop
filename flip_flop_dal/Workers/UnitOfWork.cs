using System;
using System.Collections.Generic;
using System.Text;
using flip_flop_dal.Repositories;
using Microsoft.EntityFrameworkCore;

namespace flip_flop_dal.Workers
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FlipFlopContext dbContext;
        private bool disposed = false;

        public ITransactionRepository TransactionRepository { get;}

        public IPlayerRepository PlayerRepository { get;}

        public UnitOfWork(ITransactionRepository transactionRepository,IPlayerRepository playerRepository, FlipFlopContext context)
        {
            this.TransactionRepository = transactionRepository;
            this.PlayerRepository = playerRepository;
            this.dbContext = context;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            this.dbContext.SaveChanges();
        }
    }
}
