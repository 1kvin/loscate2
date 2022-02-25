using System;
using Loscate.Site.DbContext;
using Loscate.Site.Repository;
using Loscate.Site.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Loscate.Site.Controllers.User
{
    [Authorize]
    [Route("api/user/[controller]")]
    public class EditUserNameController : Controller
    {
        private readonly ILoscateDbRepository loscateDbContext;
        private readonly IUserService userService;

        public EditUserNameController(ILoscateDbRepository loscateDbContext, IUserService userService)
        {
            this.loscateDbContext = loscateDbContext;
            this.userService = userService;
        }

        [HttpGet]
        public string Get(string newName)
        {
            if (newName.Length > 30 || newName.Length < 10)
            {
                return "LIMIT";
            }
            try
            {
                var dbUser = userService.GetDbUser(User);
                dbUser.Name = newName;
                loscateDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            
            return "OK";
        }
    }
}