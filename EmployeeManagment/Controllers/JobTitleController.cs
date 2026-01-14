using EmployeeManagment.Context;
using EmployeeManagment.DTOs.JobTitle;
using EmployeeManagment.Entities;
using EmployeeManagment.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobTitleController : ControllerBase
    {
        private readonly IJobTitleRepository _repo;

        public JobTitleController(IJobTitleRepository repo)
        {
            _repo = repo;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var jobTitles = _repo.GetAll()
                .Select(j => new { j.Id, j.Title })
                .ToList();
            return Ok(jobTitles);
        }


        [HttpPost]
        public async Task<IActionResult> Create(JobTitleDto dto)
        {
           
            var jobTitle = new JobTitle { Title = dto.Name };
            _repo.Add(jobTitle);

            return Created();
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, JobTitleDto dto)
        {
            var jobTitle = _repo.GetById(id);
            if (jobTitle is null)
                return NotFound("Job title not found");

            jobTitle.Title = dto.Name ;
            _repo.Update(jobTitle);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var jobTitle = _repo.GetById(id);

            if (jobTitle is null)
                return NotFound("Job title not found");


            if (jobTitle.Employees is null || !jobTitle.Employees.Any())
                return BadRequest("Cannot delete job title with assigned employees.");

            _repo.Delete(jobTitle);

            return NoContent();
        }

    }
}
