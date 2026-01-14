using EmployeeManagment.Context;
using EmployeeManagment.DTOs.Department;
using EmployeeManagment.Entities;
using EmployeeManagment.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _repo;

        public DepartmentController(IDepartmentRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("all_data")]
        public IActionResult GetDepartmentsWithJobTitles()
        {
            var result = _repo.GetAllWithEmployees()
            .Select(d => new
            {
                d.Id,
                d.Name,
                JobTitles = d.Employees
                    .GroupBy(e => new { e.JobTitle.Id, e.JobTitle.Title })
                    .Select(g => new
                    {
                        JobTitleId = g.Key.Id,
                        JobTitleName = g.Key.Title,
                        Employees = g.Select(e => new
                        {
                            e.Id,
                            e.FirstName,
                            e.LastName,
                            e.Email
                        })
                    })
            })
            .ToList();

            return Ok(result);
        }


        [HttpGet("employees")]
        public IActionResult GetDepartmentsWithEmployees()
        {
            var result =  _repo.GetAllWithEmployees()
                .Select(d => new
                {
                    d.Id,
                    d.Name,
                    Employees = d.Employees.Select(e => new
                    {
                        e.Id,
                        e.FirstName,
                        e.LastName,
                        JobTitle = e.JobTitle.Title,
                    })
                })
                .ToList();

            return Ok(result);
        }


        [HttpPost]
        public IActionResult Create(DepartmentDto dto)
        {
            var department = new Department { Name = dto.Name };
            _repo.Add(department);

            return Created();
        }


        [HttpPut("{id}")]
        public IActionResult Update(Guid id,DepartmentDto dto)
        {
            var department =  _repo.GetById(id);
            if (department is null) 
                return NotFound("Department not found");

            department.Name = dto.Name;
            
            _repo.Update(department);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteDepartment(Guid id)
        {
            var department =  _repo.GetById(id);

            if (department is null)
                return NotFound("Department not found");

            if( department.Employees is null || !department.Employees.Any() )
                return BadRequest("Cannot delete department with assigned employees.");

            _repo.Delete(department);

            return NoContent();
        }
    }
}
