using EmployeeManagment.Context;
using EmployeeManagment.DTOs.Employee;
using EmployeeManagment.Entities;
using EmployeeManagment.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IDepartmentRepository _departmentRepo;
        private readonly IJobTitleRepository _jobTitleRepo;

        public EmployeeController(
            IEmployeeRepository employeeRepo,
            IDepartmentRepository departmentRepo,
            IJobTitleRepository jobTitleRepo)
        {
            _employeeRepo = employeeRepo;
            _departmentRepo = departmentRepo;
            _jobTitleRepo = jobTitleRepo;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var employees = _employeeRepo.GetAll()
                .Select(e => new
                {
                    e.Id,
                    e.FirstName,
                    e.LastName,
                    e.Email,
                    Department = e.Department.Name,
                    JobTitle = e.JobTitle.Title
                }).ToList();

            return Ok(employees);
        }

        [HttpPost]
        public  IActionResult Create(EmployeeCreateDto dto)
        {
            if (_departmentRepo.GetById(dto.DepartmentId) == null)
                return BadRequest("Invalid Department Id");

            if ( _jobTitleRepo.GetById(dto.JobTitleId)==null)
                return BadRequest("Invalid JobTitle Id");

            var employee = new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                DepartmentId = dto.DepartmentId,
                JobTitleId = dto.JobTitleId
            };

            _employeeRepo.Add(employee);
            return Created();
        }

        [HttpPatch("{id}")]
        public IActionResult Update(Guid id, EmployeeUpdateDto dto)
        {
            var employee = _employeeRepo.GetById(id);
            if (employee is null) 
                return NotFound();

            if (dto.FirstName is not null)
                employee.FirstName = dto.FirstName;

            if (dto.LastName is not null)
                employee.LastName = dto.LastName;

            if (dto.DepartmentId.HasValue)
                employee.DepartmentId = dto.DepartmentId.Value;

            if (dto.JobTitleId.HasValue)
                employee.JobTitleId = dto.JobTitleId.Value;

            _employeeRepo.Update(employee);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var employee = _employeeRepo.GetById(id);
            if (employee is null) 
                return NotFound("Employee Not Found");

            _employeeRepo.Delete(employee);

            return NoContent();
        }
    }

}
