using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyApp.Core.Entities
{
    public class UserHobby : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid HobbyId { get; set; }

        public string SkillLevel { get; set; } = string.Empty; 
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        [JsonIgnore]
        public User User { get; set; } = null!;
        [JsonIgnore]
        public Hobby Hobby { get; set; } = null!;
    }
}
