using Refit;
using Server.API.SDK.Endpoints.DeleteUser;
using Server.API.SDK.Endpoints.IsEmailAvailable;
using Server.API.SDK.Endpoints.LoginUser;
using Server.API.SDK.Endpoints.RegisterUser;

namespace Server.API.SDK;

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