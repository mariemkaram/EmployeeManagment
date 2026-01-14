using Microsoft.AspNetCore.Identity;

namespace EmployeeManagment.Entities
{
    public class ApplicationUser:IdentityUser<Guid>
    {
        public Employee? Employee { get; set; }
        public Guid EmployeeId { get; set; }


    }
}
