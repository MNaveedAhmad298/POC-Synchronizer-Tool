using System.Text.Json.Serialization;

namespace MyApp.SyncAPI.Models
{
    public class UserHobby : BaseEntity
    {
        // Composite primary key
        public Guid UserId { get; set; }
        public Guid HobbyId { get; set; }

        // Meaningful property
        public string SkillLevel { get; set; } = string.Empty; 
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public User User { get; set; } = null!;
        public Hobby Hobby { get; set; } = null!;
    }
}
