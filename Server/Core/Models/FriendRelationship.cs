﻿using Server.Common.Core;

namespace Server.Core.Models;

public sealed class FriendRelationship : IAuditInformation
{
    public Guid SenderId { get; set; }
    public IdentityUser Sender { get; set; }
    
    public Guid ReceiverId { get; set; }
    public IdentityUser Receiver { get; set; }
    
    public DateTime? AcceptedAt { get; set; }
    
    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset? ModifiedOn { get; set; }
}