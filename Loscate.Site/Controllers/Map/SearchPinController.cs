using System.Collections.Generic;
using System.Linq;
using Loscate.Site.DbContext;
using Loscate.Site.Repository;
using Loscate.Site.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pin = Loscate.DTO.Map.Pin;

namespace Loscate.Site.Controllers.Map
{
    [Authorize]
    [Route("api/map/[controller]")]
    public class SearchPinController : Controller
    {
        private readonly ILoscateDbRepository loscateDbContext;

        public SearchPinController(ILoscateDbRepository loscateDbContext)
        {
            this.loscateDbContext = loscateDbContext;
        }

        [HttpGet]
        public List<Pin> Get(string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return loscateDbContext.Pins.Include(p => p.User).ToList().Select(pin => pin.ConvertToDto()).ToList();
            }
            else
            {
                return loscateDbContext.Pins
                    .Include(p => p.User)
                    .ToList()
                    .Select(pin => pin.ConvertToDto())
                    .Where(pin => (pin.ShortName.Contains(filter) || pin.FullName.Contains(filter)))
                    .ToList();
            }
        }
    }
}