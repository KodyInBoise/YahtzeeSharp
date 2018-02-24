using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee.Utilities
{
    class ResultsModel
    {
        public int Id { get; set; }
        public string Player { get; set; }
        public int Score { get; set; }
        public string Date { get; set; }
    }
}
