﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Core.Models;

namespace Server.Persistence.Configurations;

internal sealed class FriendRelationshipConfiguration : IEntityTypeConfiguration<FriendRelationship>
{
    public void Configure(EntityTypeBuilder<FriendRelationship> builder)
    {
        builder.Property(x => x.AcceptedAt)
            .IsRequired(false);
        
        builder.HasKey(x => new {x.ReceiverId, x.SenderId});
        
        builder.HasOne(x => x.Sender)
            .WithMany(x => x.FriendRelationships)
            .HasForeignKey(x => x.SenderId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.OwnsMany(x => x.Messages, navigationBuilder => {
            navigationBuilder.WithOwner();
            
            navigationBuilder.ToJson();
        });
    }
}