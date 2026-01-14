namespace EmployeeManagment.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Department? Department { get; set; }
        public Guid DepartmentId { get; set; }
        public JobTitle? JobTitle { get; set; }
        public Guid JobTitleId { get; set; }
        public Guid UserId { get; set; }

        public ApplicationUser ? User { get; set; }
    }
}
