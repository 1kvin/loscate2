using System.Linq;
using System.Security.Claims;
using Loscate.Site.DbContext;
using Loscate.Site.Repository;
using Loscate.Site.Utilities;

namespace Loscate.Site.Services
{
    public class UserService
    {
        private readonly ILoscateDbRepository loscateDbContext;
        
        public UserService(ILoscateDbRepository loscateDbContext)
        {
            this.loscateDbContext = loscateDbContext;
        }
        public FirebaseUser GetDbUser(ClaimsPrincipal user)
        {
            var firebaseUser = user.ToFirebaseUser();

            var dbUser = loscateDbContext.FirebaseUsers.SingleOrDefault(u => u.Uid == firebaseUser.UID);

            if (dbUser == null)
            {
                dbUser = firebaseUser.ConvertToDb();
                loscateDbContext.FirebaseUsers.Add(dbUser);
                loscateDbContext.SaveChanges();
            }

            return dbUser;
        }
    }
}