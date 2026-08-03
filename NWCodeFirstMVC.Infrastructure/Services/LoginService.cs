using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using NWCodeFirstMVC.Domain.Contracts;
using NWCodeFirstMVC.Infrastructure;
using NWCodeFirstMVC.Domain.PocoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NWCodeFirstMVC.Infrastructure.Services
{
    public class LoginService : GenericService<User>, ILoginService
    {
        private readonly PgNwContext _dc;
        public LoginService(PgNwContext dc) : base(dc)
        {
            this._dc = dc;
        }
        [HttpPost]
        public async Task<IActionResult> Authenticate(User userModel)
        {
            if (userModel == null || string.IsNullOrEmpty(userModel.UserName) || string.IsNullOrEmpty(userModel.Passowrd))
            {
                return new BadRequestObjectResult("Invalid input data.");
            }
            //userModel.Passowrd = BCrypt.Net.BCrypt.HashPassword(userModel.Passrd);
            var userDetails = await _dc.Users
            .FirstOrDefaultAsync(x => x.Username == userModel.UserName && x.Password == userModel.Passowrd);



            if (userDetails == null)
            {
                return new UnauthorizedObjectResult("Invalid username or password.");
            }

            return new OkObjectResult(new
            {
                Message = "Authentication successful.",
                User = userDetails
                // Token = token // Uncomment if a token is generated
            });

        }

    }
}
