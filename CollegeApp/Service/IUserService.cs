using CollegeApp.DTO;

namespace CollegeApp.Service
{
    public interface IUserService
    {

        PasswordHashDTO CreatePasswordHashWithSalt(string password);

        Task<bool> CreateUser(UserDTO userDTO);
    }
}
