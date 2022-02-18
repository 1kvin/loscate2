using System;
using Loscate.Site.DbContext;
using Loscate.Site.Repository;
using Loscate.Site.Services;
using Loscate.Site.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Loscate.Site.Controllers.User
{
    [Authorize]
    [Route("api/user/[controller]")]
    public class EditPhotoController : Controller
    {
        private readonly ILoscateDbRepository loscateDbContext;
        private readonly UserService userService;

        public EditPhotoController(ILoscateDbRepository loscateDbContext, UserService userService)
        {
            this.loscateDbContext = loscateDbContext;
            this.userService = userService;
        }

        [HttpPost]
        public string Post(string photo)
        {
            if (photo.Length / 1024 / 1024 > 20)
            {
                return "LIMIT";
            }
            try
            {
                var dbUser = userService.GetDbUser(User);
                dbUser.PhotoBase64 = photo;
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