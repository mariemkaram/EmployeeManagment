namespace EmployeeManagment.DTOs.Employee
{
    public class EmployeeCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid JobTitleId { get; set; }
    }
}
