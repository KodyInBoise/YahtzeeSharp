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
        public bool CountsBonus { get; set; }
        public bool Used { get; set; }

        public Score(string _name, bool _bonus = false)
        {
            Name = _name;
            Value = 0;
            CountsBonus = _bonus;
            Used = false;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
