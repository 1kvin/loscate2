using System.Security.Claims;
using Loscate.Site.DbContext;

namespace Loscate.Site.Services
{
    public interface IUserService
    {
        public FirebaseUser GetDbUser(ClaimsPrincipal user);
    }
}