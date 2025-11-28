namespace MyApp.SyncAPI.Models
{
    public class Hobby : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? ProficiencyLevel { get; set; }

        public ICollection<UserHobby> UserHobbies { get; set; } = new List<UserHobby>();
        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
