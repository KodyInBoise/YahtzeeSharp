using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yahtzee.Utilities;

namespace Yahtzee.Scorecard
{
    public class Numbers
    {
        List<int> Used = new List<int>();
        List<int> Available = new List<int>();

        public Numbers()
        {
            var x = 1;
            while (x < 7)
            {
                Available.Add(x);
                x++;
            }
        }

        public Score CountNumbers(List<DieModel> _diceList, int _number)
        {
            //If the player has this number available, get sum of common number and create new score
            if (NumberAvailable(_number))
            {
                var _sum = 0;
                foreach (DieModel _die in _diceList)
                {
                    if (_die.Value == _number) _sum += _die.Value;
                }
                var _newScore = new Score
                {
                    Name = $"{_number}'s",
                    Value = _sum
                };

                Used.Add(_number);
                Available.Remove(_number);

                return _newScore;
            }

            //If the player has already used the number, return null
            return null;
        }

        public bool NumberAvailable(int _number)
        {
            if (Used.Contains(_number)) return false;
           
            return true;
        }
    }
}
