using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Models;
using MyAPI.Services;

namespace MyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _employeeService;

        public UserController(IUserService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _employeeService.GetUsersList();

            return Ok(result);
        }

        //[HttpGet("{id:int}")]
        //public async Task<IActionResult> GetEmployee(int id)
        //{
        //    var result = await _employeeService.GetUsersList(id);

        //    return Ok(result);
        //}

        [HttpPost]
        public async Task<IActionResult> AddUsers([FromBody] Users employee)
        {
            var result = await _employeeService.CreateUsers(employee);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromBody] Users employee)
        {
            var result = await _employeeService.UpdateUsers(employee);

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await _employeeService.DeleteUsers(id);

            return Ok(result);
        }
    }
}
