namespace EmployeeReader.Models
{
  public class Employee
  {
    public int id { get; set; }
    public string? first_name { get; set; }
    public string? last_name { get; set; }
    public DateTime? birth_date { get; set; }
    public int? department_id { get; set; }
  }
}
