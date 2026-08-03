using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NWCodeFirstMVC.Domain.Contracts;
using NWCodeFirstMVC.Domain.Dto;
using NWCodeFirstMVC.Domain.PocoModels;
using NWCodeFirstMVC.Infrastructure.Services;

namespace NWCodeFirstMVC.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IMapper mapper;

        public LoginController(ILoginService loginService, IMapper mapper)
        {
            _loginService = loginService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var user = await _loginService.GetAllAsync();
            return Ok(user);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _loginService.GetAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Authentication(User userModel)
        {
            try
            {
                var user = await _loginService.Authenticate(userModel);
                if (user == null) return Unauthorized("Invalid credentials.");
                return Ok(user);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddProduct(User createUser)
        {
            var user = mapper.Map<User>(createUser);
            var results = await _loginService.AddAsync(user);
            return Ok(user);
        }
    }
}
