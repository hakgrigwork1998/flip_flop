using flip_flop_dal.Enitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace flip_flop_dal.EntityConfigrations
{
    public class TransactionTypeConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder
                .ToTable("transaction");

            builder
                .HasKey(p => p.TransactionId)
                .HasName("transaction_id");

            builder
                .Property(p => p.TransactionId)
                .HasColumnName("transaction_id");

            builder
                .Property(p => p.TransactionId)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder
                .Property(p => p.GeneratedTransactionId)
                .HasColumnName("generated_transaction_id")
                .IsRequired();

            builder
                .Property(p => p.BetAmount)
                .HasColumnName("bet_amount")
                .HasColumnType("smallmoney")
                .IsRequired();

            builder
                .Property(p => p.WinAmount)
                .HasColumnType("smallmoney")
                .HasColumnName("win_amount")
                .IsRequired();

            builder.Property(p => p.Currency)
                .HasColumnName("currency")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(p => p.TransactionDateTime)
                .HasColumnName("date_time")
                .IsRequired();

            builder
                .Property(p => p.IsSucceeded)
                .HasColumnName("is_succedded")
                .IsRequired();

            builder
                .Property(p => p.GameId)
                .HasColumnName("game_id")
                .IsRequired();

            builder
                .HasOne(t => t.Player)
                .WithMany(p => p.Transactions)
                .HasForeignKey(p => p.PlayerId);

            builder.Property(p => p.PlayerId).HasColumnName("player_id").IsRequired();
        }
    }
}
