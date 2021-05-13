using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WinnerController : Controller
    {
        // GET: WinnerController
        [HttpGet]
        public JsonResult Get(PlayerHand[] playerHands)
        {
            var winner = PokerAPI.Helpers.PokerHelpers.DetermineWinner(playerHands);
            return Json(winner);
        }
    }
}
