using System.Security.Claims;
using WebApiAuth.Models;
using WebApiAuth.Respository.Interface;

namespace WebApiAuth.Respository.Implementation
{
    public class UserService : IUserServicecs
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetMyName()
        {
           var result = string.Empty;
            if (_httpContextAccessor.HttpContext!=null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            return result;
        }
    }
}
