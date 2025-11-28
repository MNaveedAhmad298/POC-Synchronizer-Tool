using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyApp.Core.Entities
{
    public class User : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }

        [JsonIgnore]
        public ICollection<Hobby> Hobbies { get; set; } = new List<Hobby>();

        [JsonIgnore]
        public ICollection<UserHobby> UserHobbies { get; set; } = new List<UserHobby>();
    }
}
