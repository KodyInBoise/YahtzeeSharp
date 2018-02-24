using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee.ScoreHelper
{
    public class ScoreResult
    {
        public string Name { get; set; }
        public int? PlayerOne { get; set; }
        public int? PlayerTwo { get; set; }
        public int? PlayerThree { get; set; }
        public int? PlayerFour { get; set; }
        public int? PlayerFive { get; set; }
    }
}
