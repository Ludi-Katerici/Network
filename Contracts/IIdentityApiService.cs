using Contracts.Endpoints.DeleteUser;
using Contracts.Endpoints.IsEmailAvailable;
using Contracts.Endpoints.LoginUser;
using Contracts.Endpoints.RegisterUser;
using Refit;

namespace Contracts;

public interface IIdentityApiService
{
    [Get(IsEmailAvailableRequest.Route)]
    Task<ApiResponse<bool>> IsEmailAvailable(string email);
    
    [Post(RegisterUserRequest.Route)]
    Task<ApiResponse<RegisterUserResponse>> RegisterUser([Body] RegisterUserRequest request);
    
    [Post(LoginUserRequest.Route)]
    Task<ApiResponse<LoginUserResponse>> LoginUser([Body] LoginUserRequest request);
    
    [Delete(DeleteUserRequest.Route)]
    Task<IApiResponse> DeleteUser(Guid userId);
}