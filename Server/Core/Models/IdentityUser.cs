﻿using Server.Common.Core;
using Server.Common.Core.Abstract;

namespace Server.Core.Models;

public sealed class IdentityUser : Entity<Guid>, IAggregateRoot, IAuditInformation
{
    private HashSet<LoginInfo> logins = [];
    public IdentityUser(
        string email) : base(Guid.NewGuid())
    {
        this.Email = email;
        this.AuthorizationDetails = new AuthorizationDetails();
    }

    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public AuthorizationDetails AuthorizationDetails { get; private set; }

    public IReadOnlyCollection<LoginInfo> Logins => this.logins.ToList().AsReadOnly();

    #region Methods

    public void AddLoginInfo(LoginInfo loginInfo)
    {
        this.logins ??= new HashSet<LoginInfo>();

        if (this.logins.Count == 10)
        {
            var oldestLogin = this.logins.OrderBy(x => x.OccurredOn).First();
            this.logins.Remove(oldestLogin);
        }

        this.logins.Add(loginInfo);
    }

    #endregion

    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset? ModifiedOn { get; set; }

    private IdentityUser() {}
}