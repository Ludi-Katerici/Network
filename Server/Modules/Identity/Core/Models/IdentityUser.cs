using Server.Common.Core;
using Server.Common.Core.Abstract;

namespace Server.Modules.Identity.Core.Models;

public sealed class IdentityUser : Entity<Guid>, IAggregateRoot, IAuditInformation
{
    private HashSet<LoginInfo> logins = [];
    public IdentityUser(
        string email,
        DateOnly? scheduledActivationDateDate = null) : base(Guid.NewGuid())
    {
        this.Email = email;
        this.ScheduledActivationDate = scheduledActivationDateDate;
        this.AuthorizationDetails = new AuthorizationDetails();
    }

    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public AuthorizationDetails AuthorizationDetails { get; private set; }

    public DateOnly? ScheduledActivationDate { get; private set; }
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