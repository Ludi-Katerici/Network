using Refit;
using Server.Modules.Identity.API.SDK.Endpoints.DeleteUser;
using Server.Modules.Identity.API.SDK.Endpoints.IsEmailAvailable;
using Server.Modules.Identity.API.SDK.Endpoints.LoginUser;
using Server.Modules.Identity.API.SDK.Endpoints.RegisterUser;

namespace Server.Modules.Identity.API.SDK;

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