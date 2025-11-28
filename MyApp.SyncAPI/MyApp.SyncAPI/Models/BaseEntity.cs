namespace MyApp.SyncAPI.Models
{
    public class BaseEntity
    {
        public bool IsDeleted { get; set; } = false;
        public bool IsSynced { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
