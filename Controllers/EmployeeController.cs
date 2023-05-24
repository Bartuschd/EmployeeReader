using EmployeeReader.Repositories;
using EmployeeReader.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
    public async Task<ActionResult<List<Employee>>> Read()
    {
      try
      {
        var employees = await _repository.GetAllAsync();
        if (employees == null)
        {
          return NoContent();
        }
        return Ok(employees);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }

    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> Read(int id)
    {
      try
      {
        var employee = await _repository.GetByIdAsync(id);
        if (employee == null)
        {
          return NoContent();
        }
        return Ok(employee);
        }
	    catch (System.Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost]
    public async Task<ActionResult<Employee>> Create(Employee employee)
    {
      try
      {
        await _repository.CreateAsync(employee);
        if (employee == null)
        {
          return NoContent();
        }
        return Ok(employee);
      }
      catch (System.Exception ex)
      {
        return BadRequest(ex.Message);
      }

    }
    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<Employee>> Update(Employee employee)
    {
      try
      {
        await _repository.UpdateAsync(employee);
        if (employee == null) 
        { 
          return NoContent();
        }
        return Ok(employee);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }

    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
      await _repository.DeleteAsync(id);
      return NoContent();
    }
  }
}
