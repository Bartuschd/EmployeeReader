using EmployeeReader.Models;
using Npgsql;
using System.Reflection.Metadata.Ecma335;

namespace EmployeeReader.Repositories
{
  public class EmployeeRepository
  {
    private readonly string _connectionString;

    public EmployeeRepository(string connectionString)
    {
      _connectionString = connectionString;
    }


    public async Task<List<Employee>> GetAllAsync()
    {
      var employees = new List<Employee>();

      using (var connection = new NpgsqlConnection(_connectionString))
      {
        await connection.OpenAsync();

        using (var command = new NpgsqlCommand("SELECT * FROM employees", connection))
        {
          using (var reader = await command.ExecuteReaderAsync())
          {
            while (await reader.ReadAsync())
            {
              var employee = new Employee
              {
                id = reader.GetInt32(reader.GetOrdinal("id")),
                first_name = reader.GetString(reader.GetOrdinal("first_name")),
                last_name = reader.GetString(reader.GetOrdinal("last_name")),
                birth_date = reader.GetDateTime(reader.GetOrdinal("birth_date")),
                department_id = reader.GetInt32(reader.GetOrdinal("department_id"))
              };

              employees.Add(employee);
            }
          }
        }
        await connection.CloseAsync();
      }

      return employees;
    }

    public async Task<Employee> GetByIdAsync(int id)
    {
      Employee employee = null;

      using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
      {
        await connection.OpenAsync();

        using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM employees WHERE id = @id", connection))
        {
          command.Parameters.AddWithValue("@id", id);

          using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
          {
            if (await reader.ReadAsync())
            {
               employee = new Employee
              {
                id = reader.GetInt32(reader.GetOrdinal("id")),
                first_name = reader.GetString(reader.GetOrdinal("first_name")),
                last_name = reader.GetString(reader.GetOrdinal("last_name")),
                birth_date = reader.GetDateTime(reader.GetOrdinal("birth_date")),
                department_id = reader.GetInt32(reader.GetOrdinal("department_id"))
              };
            }
          }
        }
        await connection.CloseAsync();
      }
      return employee;
    }

    public async Task CreateAsync(Employee employee)
    {
      using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
      {
        await connection.OpenAsync();

        using (NpgsqlCommand command = new NpgsqlCommand("INSERT INTO employees (first_name, last_name, birth_date, department_id) VALUES (@first_name, @last_name, @birth_date, @department_id)", connection))
        {
          command.Parameters.AddWithValue("@first_name", employee.first_name);
          command.Parameters.AddWithValue("@last_name", employee.last_name);
          command.Parameters.AddWithValue("@birth_date", employee.birth_date);
          command.Parameters.AddWithValue("@department_id", employee.department_id);

          await command.ExecuteNonQueryAsync();
        }
      }
    }

    public async Task UpdateAsync(Employee employee)
    {
      using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
      {
        await connection.OpenAsync();

        using (NpgsqlCommand command = new NpgsqlCommand("UPDATE employees SET first_name = @first_name, last_name = @last_name, birth_date = @birth_date, department_id = @department_id WHERE id = @id", connection))
        {
          command.Parameters.AddWithValue("@first_name", employee.first_name);
          command.Parameters.AddWithValue("@last_name", employee.last_name);
          command.Parameters.AddWithValue("@birth_date", employee.birth_date);
          command.Parameters.AddWithValue("@department_id", employee.department_id);
          command.Parameters.AddWithValue("@id", employee.id);

          await command.ExecuteNonQueryAsync();
        }
      }
    }

    public async Task DeleteAsync(int id)
    {
      using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
      {
        await connection.OpenAsync();

        using (NpgsqlCommand command = new NpgsqlCommand("DELETE FROM employees WHERE id = @id", connection))
        {
          command.Parameters.AddWithValue("@id", id);

          await command.ExecuteNonQueryAsync();
        }
        await connection.CloseAsync();
      }
    }
  }
}
