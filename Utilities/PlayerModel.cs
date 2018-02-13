using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yahtzee.Scorecard;

namespace Yahtzee.Utilities
{
    public class PlayerModel
    {
        public string Name { get; set; }
        public ScoreTracker Scorecard { get; set; }
    }
}
