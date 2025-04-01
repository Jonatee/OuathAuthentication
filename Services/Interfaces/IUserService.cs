using OuathAuthentication.Models;

namespace OuathAuthentication.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseModel> RegisterUser(UserRequestModel request);
        Task<UserResponseModel> LoginUser(LoginRequestModel request);
    }
}
