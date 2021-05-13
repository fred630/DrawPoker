using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokerAPI.Helpers;

namespace PokerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DealController : Controller
    {
        // GET: DealController
        [HttpGet]
        public JsonResult Get(string playerOne, string playerTwo)
        {
            var hands = PokerHelpers.DealCards(playerOne, playerTwo);
            return Json(hands);
        }
    }
}
