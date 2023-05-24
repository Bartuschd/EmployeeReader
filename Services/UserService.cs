using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeReader.Services
{
  public class UserService :IUserService
  {
    public bool ValidateCredentials(string username, string password)
    {
      return username.Equals("admin") && password.Equals("password");
    }
  }
}
