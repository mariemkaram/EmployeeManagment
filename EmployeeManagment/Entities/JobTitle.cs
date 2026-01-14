namespace EmployeeManagment.Entities
{
    public class JobTitle
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public ICollection<Employee>? Employees { get; set; }
    }
}
