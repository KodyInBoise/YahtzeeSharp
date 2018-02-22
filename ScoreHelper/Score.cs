using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yahtzee.Utilities;

namespace Yahtzee.Scorecard
{
    public class Score
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public bool Used { get; set; }
    }
}
