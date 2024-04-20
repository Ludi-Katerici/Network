using Server.Common.Core;
using Server.Common.Core.Abstract;

namespace Server.Core.Models;

public sealed class IdentityUser : Entity<Guid>, IAuditInformation
{
    private HashSet<LoginInfo> logins = [];
    public IdentityUser(
        string name,
        string family,
        string email,
        string phoneNumber,
        string region,
        string city,
        string education,
        string work,
        string professionalExperience,
        string interests,
        string searchings,
        string additionalInformation) : base(Guid.NewGuid())
    {
        this.Name = name;
        this.Family = family;
        this.Email = email.ToUpper();
        this.Education = education;
        this.Work = work;
        this.PhoneNumber = phoneNumber;
        this.AuthorizationDetails = new AuthorizationDetails();
        this.Region = region;
        this.City = city;
        this.ProfessionalExperience = professionalExperience;
        this.Interests = interests;
        this.Searchings = searchings;
        this.AdditionalInformation = additionalInformation;
    }

    public string Name { get; set; }
    public string Family { get; set; }

    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string PasswordHash { get; set; }

    public string Region { get; set; }
    public string City { get; set; }
    
    public string Education { get; set; }
    public string Work { get; set; }

    public string ProfessionalExperience { get; set; }

    public string Interests { get; set; }

    public string Searchings { get; set; }

    public string AdditionalInformation { get; set; }
    
    public List<FriendRelationship> FriendRelationships { get; set; } = new();

    public AuthorizationDetails AuthorizationDetails { get; private set; }
    
    public List<EventIdentityUser> EventsAttended { get; set; } = new(); 
    
    public List<Event> EventsCreated { get; set; } = new();

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