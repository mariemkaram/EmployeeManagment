namespace EmployeeManagment.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime Expires { get; set; }
        public DateTime? Revoked { get; set; }
        public bool IsExpired => Expires <= DateTime.UtcNow;
        public bool IsActive => Revoked == null && !IsExpired;

        public ApplicationUser? User { get; set; }

    }
}
