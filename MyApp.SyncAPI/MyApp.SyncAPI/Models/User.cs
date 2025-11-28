namespace MyApp.SyncAPI.Models
{
    public class User : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int Age { get; set; }

        public ICollection<Hobby>? Hobbies { get; set; }
        public ICollection<UserHobby> UserHobbies { get; set; } = new List<UserHobby>();
    }
}
