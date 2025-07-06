using Contracts.Dtos;
using Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebAPI_FirmTaskProject.Controllers
{
    [ApiController]
    [Route("/employees")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeDto>>> GetAll([FromQuery] EmployeeQueryParameters query)
        {
            return await _employeeService.GetAllAsync(query);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto employeeDto)
        {
            await _employeeService.CreateAsync(employeeDto);
            return Ok();
        }

        [HttpPut("/{id:guid}")]
        public async Task<IActionResult> UpdateEmployee(
            [FromRoute] Guid id,
            [FromBody] EmployeeDto employeeDto)
        {
            await _employeeService.UpdateAsync(id, employeeDto);
            return Ok();
        }

        [HttpDelete("/{id:guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            await _employeeService.DeleteAsync(id);
            return Ok();
        }
    }
}
