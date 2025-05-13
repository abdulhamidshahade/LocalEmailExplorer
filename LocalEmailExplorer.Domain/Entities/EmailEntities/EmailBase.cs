namespace LocalEmailExplorer.Domain.Entities.EmailEntities
{ 
    public abstract class EmailBase
    {
        public string EmailAddress { get; set; }
        public string? Password { get; set; }
        public string? Phone { get; set; }
        public string? RecoveryEmail { get; set; } 
        public bool IsActive { get; set; }
        public bool IsAvailable { get; set; }
    }
}
