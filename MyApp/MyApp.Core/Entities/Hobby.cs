using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MyApp.Core.Entities
{
    public class Hobby : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public string? ProficiencyLevel { get; set; }

        public Guid UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; } = null!;

        [JsonIgnore]
        public ICollection<UserHobby> UserHobbies { get; set; } = new List<UserHobby>();
    }
}
