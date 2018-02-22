using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yahtzee.Scorecard;
using Yahtzee.Utilities;

namespace Yahtzee.ScoreHelper
{
    class VerifyScore
    {
        public static Score NumberScore(ScoreTracker _scores, List<DieModel> _diceList, int _number)
        {
            if (!_scores.AvailableNumbers.Contains(_number)) return null;

            else
            {
                var _sum = 0;
                foreach (DieModel _die in _diceList)
                {
                    if (_die.Value == _number)
                    {
                        _sum += _die.Value;
                    }
                }

                var _numberScore = new Score
                {
                    Name = $"{_number}'s",
                    Value = _sum
                };

                return _numberScore;
            }
        }

        public static bool ThreeOfAKind(List<DieModel> _diceList)
        {
            var x = 0;
            foreach (DieModel _die in _diceList)
            {
                foreach (DieModel _compareDie in _diceList)
                {
                    if (_compareDie.Value == _die.Value)
                    {
                        x++;
                    }
                }

                if (x >= 3)
                {
                    return true;
                }
                else
                {
                    x = 0;
                }
            }

            return false;
        }

        public static bool FourOfAKind(List<DieModel> _diceList)
        {
            var x = 0;
            foreach (DieModel _die in _diceList)
            {
                foreach (DieModel _compareDie in _diceList)
                {
                    if (_compareDie.Value == _die.Value)
                    {
                        x++;
                    }
                }

                if (x >= 4)
                {
                    return true;
                }
                else
                {
                    x = 0;
                }
            }

            return false;
        }

        public static bool FullHouse(List<DieModel> _diceList)
        {
            var _exemptDice = new List<DieModel>();
            var x = 0;
            foreach (DieModel _die in _diceList)
            {
                foreach (DieModel _compareDie in _diceList)
                {
                    if (_compareDie.Value == _die.Value)
                    {
                        x++;
                    }
                    else
                    {
                        _exemptDice.Add(_compareDie);
                    }
                }

                if (x >= 3)
                {
                    var a = _exemptDice[0];
                    var b = _exemptDice[1];
                    if (a.Value == b.Value)
                    {
                        return true;
                    }
                }
                else
                {
                    _exemptDice = new List<DieModel>();
                    x = 0;
                }
            }

            return false;
        }

        public static bool SmallStraight(List<DieModel> _diceList)
        {
            var _values = _diceList.Select(x => x.Value);
            if (_values.Contains(1) && _values.Contains(2) && _values.Contains(3) && _values.Contains(4))
            {
                return true;
            }
            if (_values.Contains(2) && _values.Contains(3) && _values.Contains(4) && _values.Contains(5))
            {
                return true;
            }
            if (_values.Contains(3) && _values.Contains(4) && _values.Contains(5) && _values.Contains(6))
            {
                return true;
            }

            return false;
        }

        public static bool LargeStraight(List<DieModel> _diceList)
        {
            var _values = _diceList.Select(x => x.Value);
            if (_values.Contains(1) && _values.Contains(2) && _values.Contains(3) && _values.Contains(4) && _values.Contains(5))
            {
                return true;
            }
            if (_values.Contains(2) && _values.Contains(3) && _values.Contains(4) && _values.Contains(5) && _values.Contains(6))
            {
                return true;
            }

            return false;
        }
    }
}
