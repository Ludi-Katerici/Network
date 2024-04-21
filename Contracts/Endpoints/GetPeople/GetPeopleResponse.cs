namespace Contracts.Endpoints.GetPeople;

public sealed class GetPeopleResponse
{
    public List<Person> People { get; set; } = [];
    
    public class Person
    {
        public required Guid Id { get; set; }
        public required string ProfilePictureUrl { get; set; }
        public required string Name { get; set; }
        public required string Family { get; set; }
        public required string Email { get; set; }
        public required string Region { get; set; }
        public required string Education { get; set; }
        public required string Work { get; set; }
        public required string ProfessionalExperience { get; set; }
        public required List<string> Interests { get; set; }
        public required List<string> Searchings { get; set; }
        public required string AdditionalInformation { get; set; }
        public required int FriendsCount { get; set; }
        public required List<AttendedEvent> AttendedEvents { get; set; }
    }
    
    public class AttendedEvent
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required DateTime StartDate { get; set; }
        public required string PictureUrl { get; set; }
    }
}