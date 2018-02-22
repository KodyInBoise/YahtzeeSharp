using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yahtzee.ScoreHelper;
using Yahtzee.Utilities;

namespace Yahtzee.Scorecard
{
    public class ScoreTracker
    {
        public List<Score> ScoreList;
        public List<int> AvailableNumbers;      

        public ScoreTracker()
        {
            ScoreList = new List<Score>
            {
                new Score
                {
                    Name = "Three of a Kind", 
                    Value = 0,
                    Used = false,
                },
                new Score
                {
                    Name = "Four of a Kind",
                    Value = 0,
                    Used = false,
                },
                new Score
                {
                    Name = "Full House",
                    Value = 0,
                    Used = false,
                },
            };
            AvailableNumbers = new List<int>();
            var x = 1;
            while (x < 7)
            {
                AvailableNumbers.Add(x);
                x++;
            }
        }

        public void AddScore(List<DieModel> _diceList, int _number)
        {
            var _newScore = VerifyScore.NumberScore(this, _diceList, _number);
            if (_newScore != null)
            {
                ScoreList.Add(_newScore);
            }
        }

        public void AddThreeOfAKind(List<DieModel> _diceList)
        {
            var _valid = VerifyScore.ThreeOfAKind(_diceList);
            var _score = ScoreList.Find(s => s.Name == "Three of a Kind");
            if (VerifyScore.ThreeOfAKind(_diceList))
            {
                foreach (DieModel _die in _diceList)
                {
                    _score.Value += _die.Value;
                }
            }
            else
            {
                _score.Value = 0;
            }
            _score.Used = true;
        }

        public void AddFourOfAKind(List<DieModel> _diceList)
        {
            var _score = ScoreList.Find(s => s.Name == "Four of a Kind");
            if (VerifyScore.FourOfAKind(_diceList))
            {
                foreach (DieModel _die in _diceList)
                {
                    _score.Value += _die.Value;
                }
            }
            else
            {
                _score.Value = 0;
            }
            _score.Used = true;
        }

        public void AddFullHouse (List<DieModel> _diceList)
        {
            var _score = ScoreList.Find(s => s.Name == "Full House");
            if (VerifyScore.FullHouse(_diceList))
            {
                _score.Value = 25;
            }
            else
            {
                _score.Value = 0;
            }
            _score.Used = true;
        }
    }
}
