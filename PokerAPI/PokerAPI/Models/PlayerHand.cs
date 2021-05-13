using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokerAPI.Models
{
    public class PlayerHand
    {
        public string PlayerName { get; set; }
        public string Hand { get; set; }
        public string Discard { get; set; }
        public bool Winner { get; set; }
    }
}
