using CollegeApp.DTO;
using CollegeApp.Model;
using CollegeApp.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private ApiResponse apiResponse;
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            apiResponse = new ApiResponse();
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateUser(UserDTO userDTO) {
            try
            {
                _logger.LogInformation("Method has Started");
                var userCreated = await _userService.CreateUser(userDTO);
                apiResponse.data = userCreated;
                apiResponse.Status = true;
                apiResponse.StatusCode = HttpStatusCode.OK;
                //var StuData = await _dbContext.Students.Select(s => new StudentDTO
                //{
                //    Id = s.Id,
                //    Name = s.StudentName,
                //    Address = s.Address,
                //    Email = s.Email,
                //    DOB = s.DOB
                //}).ToListAsync();
                _logger.LogInformation("Success");
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {

                apiResponse.Status = false;
                apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                apiResponse.Errors.Add(ex.Message);
                return apiResponse;
            }

        }
    }
}
