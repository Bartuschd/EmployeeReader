using EmployeeReader.Repositories;
using EmployeeReader.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeReader.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class EmployeeController : ControllerBase
  {
    private readonly EmployeeRepository _repository;

    public EmployeeController(EmployeeRepository repository)
    {
      _repository = repository;
    }

    
    [HttpGet]
    public async Task<ActionResult<List<Employee>>> GetAll()
    {
        var employees = await _repository.GetAllAsync();
        return employees;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> GetById(int id)
    {
      var employee = await _repository.GetByIdAsync(id);
      return employee;
    }

    [HttpPost]
    public async Task<ActionResult<Employee>> Create(Employee employee)
    {
      await _repository.CreateAsync(employee);
      return employee;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Employee>> Update(int id, Employee employee)
    {
      if (id != employee.id)
      {
        return BadRequest();
      }
      await _repository.UpdateAsync(employee);
      return employee;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
      await _repository.DeleteAsync(id);
      return NoContent();
    }
  }
}
