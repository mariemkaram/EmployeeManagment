namespace EmployeeManagment.DTOs.Employee
{
    public class EmployeeUpdateDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? JobTitleId { get; set; }
    }
}
