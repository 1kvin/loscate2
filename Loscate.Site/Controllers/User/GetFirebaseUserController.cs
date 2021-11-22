using Loscate.DTO.Firebase;
using Loscate.Site.Services;
using Loscate.Site.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Loscate.Site.Controllers.User
{
    [Authorize]
    [Route("api/user/[controller]")]
    public class GetFirebaseUserController : Controller
    {
        private readonly UserService userService;
        public GetFirebaseUserController(UserService userService)
        {
            this.userService = userService;
        }
        [HttpGet]
        public FirebaseUser Get()
        {
            return userService.GetDbUser(User).ConvertToDto();
            //return User.ToFirebaseUser();
        }
    }
}