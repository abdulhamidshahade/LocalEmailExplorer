using LocalEmailExplorer.Domain.Entities.Base;

namespace LocalEmailExplorer.Domain.Entities.EmailEntities
{
    public class Email : EmailBase, IAuditableEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
