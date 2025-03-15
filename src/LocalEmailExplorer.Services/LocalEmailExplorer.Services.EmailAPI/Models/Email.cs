using System.ComponentModel.DataAnnotations;

namespace LocalEmailExplorer.Services.EmailAPI.Models.DTOs
{
    public class Email : EmailBase
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        public Email()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
