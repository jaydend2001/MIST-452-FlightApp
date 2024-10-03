using System.Security.Claims;

namespace SP24MVCDonham.Models
{
    public class AppUserRepo : IAppUserRepo
    {
        private IHttpContextAccessor httpContextAccessor;

        public AppUserRepo(IHttpContextAccessor accessor)
        {
            this.httpContextAccessor = accessor;
        }

        public string GetAppUserID()
        {
            return this.httpContextAccessor.HttpContext.User.FindFirst
                (ClaimTypes.NameIdentifier).Value;
        }
    }
}
