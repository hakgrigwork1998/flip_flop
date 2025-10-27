using flip_flop_dal.Enitites;
using flip_flop_dal.EntityConfigrations;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace flip_flop_dal
{
    public class FlipFlopContext : DbContext
    {
        public FlipFlopContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Player> Player { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PlayerTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionTypeConfiguration());
        }
    }
}
