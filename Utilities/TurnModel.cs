using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee.Utilities
{
    class TurnModel
    {
        public PlayerModel Player { get; set; }
        public int RollCount { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Result { get; set; }
    }
}
