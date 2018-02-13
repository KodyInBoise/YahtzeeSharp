using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee.Utilities
{
    public class DieModel
    {
        public int Id { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            return $"Die {Id} - {Value}";
        }
    }
}
