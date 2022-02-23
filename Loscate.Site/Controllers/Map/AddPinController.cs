using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Loscate.Site.DbContext;
using Loscate.Site.Repository;
using Loscate.Site.Services;
using Loscate.Site.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Loscate.Site.Controllers.Map
{
    [Authorize]
    [Route("api/map/[controller]")]
    public class AddPinController : Controller
    {
        private readonly ILoscateDbRepository loscateDbContext;
        private readonly IUserService userService;
        private const int MAX_PIN_PER_DAY = 3;

        public AddPinController(ILoscateDbRepository loscateDbContext, IUserService userService)
        {
            this.loscateDbContext = loscateDbContext;
            this.userService = userService;
        }

        [HttpPost]
        public string Post(string fullName, string shortName, string latitude, string longitude, string photo)
        {
            try
            {
                var dbUser = userService.GetDbUser(User);

                var nowDate = DateTime.UtcNow;
                var startDate = new DateTime(nowDate.Year, nowDate.Month, nowDate.Day, 0, 0, 0);
                var endDate = new DateTime(nowDate.Year, nowDate.Month, nowDate.Day, 23, 59, 59);

                var pinsAddedForThisDayCount = loscateDbContext.Pins.Count(p => (p.UserId == dbUser.Id) && (p.AddDate > startDate)&& (p.AddDate < endDate));

                if (pinsAddedForThisDayCount >= MAX_PIN_PER_DAY)
                {
                    return "LIMIT";
                }

                var dbPin = new Pin()
                {
                    Latitude = double.Parse(latitude.Replace(",","."), CultureInfo.InvariantCulture),
                    Longitude = double.Parse(longitude.Replace(",","."), CultureInfo.InvariantCulture),
                    UserId = dbUser.Id,
                    Photo = photo,
                    FullName = fullName,
                    ShortName = shortName,
                    AddDate = DateTime.UtcNow
                };
                
                loscateDbContext.Pins.Add(dbPin);
                loscateDbContext.SaveChanges();
                
                return "OK";
            }
            catch (Exception e)
            {
                return e.ToString() + "::" + latitude;
            }
        }
    }
}