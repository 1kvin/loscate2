using System;
using System.Linq;
using Loscate.Site.DbContext;
using Loscate.Site.Repository;
using Loscate.Site.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Loscate.Site.Controllers.Map
{
    [Authorize]
    [Route("api/map/[controller]")]
    public class DelPinController: Controller
    {
        private readonly ILoscateDbRepository loscateDbContext;
        private readonly UserService userService;

        public DelPinController(ILoscateDbRepository loscateDbContext, UserService userService)
        {
            this.loscateDbContext = loscateDbContext;
            this.userService = userService;
        }

        [HttpGet]
        public string Get(int id)
        {
            try
            {
                var dbUser = userService.GetDbUser(User);
                var pin = loscateDbContext.Pins.Single(p => p.UserId == dbUser.Id && p.Id == id);
                loscateDbContext.Pins.Remove(pin);

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