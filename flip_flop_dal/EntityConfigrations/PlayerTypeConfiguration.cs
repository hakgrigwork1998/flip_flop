using flip_flop_dal.Enitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace flip_flop_dal.EntityConfigrations
{
    public class PlayerTypeConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder
                .ToTable("player");

            builder
                .HasKey(p => p.PlayerId)
                .HasName("player_id");

            builder
             .Property(p => p.PlayerId)
             .HasColumnName("player_id");

            builder
                .Property(p => p.PlayerId)
                .ValueGeneratedNever()
                .IsRequired();

            builder
                .Property(p => p.Username)
                .HasColumnName("username")
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(p => p.ExternalId)
                .HasColumnName("external_id")
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(p => p.PartnerId)
                .HasColumnName("partner_id")
                .IsRequired();
        }
    }
}
