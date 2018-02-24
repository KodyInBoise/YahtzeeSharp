using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yahtzee.Scorecard;
using Yahtzee.ScoreHelper;
using Yahtzee.Utilities;

namespace Yahtzee.Scorecard
{
    public class ScoreTracker
    {
        public List<Score> ScoreList { get; set; }
        public List<int> AvailableNumbers { get; set; }

        public ScoreTracker()
        {
            ScoreList = new List<Score>
            {
                new Score("Aces", true),
                new Score("Twos", true),
                new Score("Threes", true),
                new Score("Fours", true),
                new Score("Fives", true),
                new Score("Sixes", true),
                new Score("3 of a kind"),
                new Score("4 of a kind"),
                new Score("Full House"),
                new Score("Small Straight"),
                new Score("Large Straight"),
                new Score("Yahtzee"),
                new Score("Chance"),
            };
        }

        public void AddScore(Score _score, List<DieModel> _diceList)
        {

        }

        public void AddOnes(List<DieModel> _diceList)
        {
            var _score = ScoreList.Find(s => s.Name == "Ones");
            var x = 0;
            foreach (DieModel _die in _diceList)
            {
                if (_die.Value == 1)
                {
                    _score.Value += 1;
                    x++;
                }
            }
            if (x < 3)
            {
                _score.Value = 0;
            }
            _score.Used = true;
        }

        public void AddTwos(List<DieModel> _diceList)
        {
            var _score = ScoreList.Find(s => s.Name == "Twos");
            var x = 0;
            foreach (DieModel _die in _diceList)
            {
                if (_die.Value == 2)
                {
                    _score.Value += 2;
                }
            }
            if (x < 3)
            {
                _score.Value = 0;
            }
            _score.Used = true;
        }

        public void AddThrees(List<DieModel> _diceList)
        {
            var _score = ScoreList.Find(s => s.Name == "Threes");
            var x = 0;
            foreach (DieModel _die in _diceList)
            {
                if (_die.Value == 3)
                {
                    _score.Value += 3;
                }
            }
            if (x < 3)
            {
                _score.Value = 0;
            }
            _score.Used = true;
        }

        public void AddFours(List<DieModel> _diceList)
        {
            var _score = ScoreList.Find(s => s.Name == "Fours");
            var x = 0;
            foreach (DieModel _die in _diceList)
            {
                if (_die.Value == 4)
                {
                    _score.Value += 4;
                }
            }
            if (x < 3)
            {
                _score.Value = 0;
            }
            _score.Used = true;
        }

        public void AddFives(List<DieModel> _diceList)
        {
            var _score = ScoreList.Find(s => s.Name == "Fives");
            var x = 0;
            foreach (DieModel _die in _diceList)
            {
                if (_die.Value == 5)
                {
                    _score.Value += 5;
                }
            }
            if (x < 3)
            {
                _score.Value = 0;
            }
            _score.Used = true;
        }

        public void AddSixes(List<DieModel> _diceList)
        {
            var _score = ScoreList.Find(s => s.Name == "Sixes");
            var x = 0;
            foreach (DieModel _die in _diceList)
            {
                if (_die.Value == 6)
                {
                    _score.Value += 6;
                }
            }
            if (x < 3)
            {
                _score.Value = 0;
            }
            _score.Used = true;
        }

        public void AddThreeOfAKind(List<DieModel> _diceList)
        {
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

        public void AddFullHouse(List<DieModel> _diceList)
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

        public void AddSmallStraight(List<DieModel> _diceList)
        {
            var _score = ScoreList.Find(s => s.Name == "Small Straight");
            if (VerifyScore.SmallStraight(_diceList))
            {
                _score.Value = 30;
            }
            else
            {
                _score.Value = 0;
            }
            _score.Used = true;
        }

        public void AddLargeStraight(List<DieModel> _diceList)
        {
            var _score = ScoreList.Find(s => s.Name == "Large Straight");
            if (VerifyScore.LargeStraight(_diceList))
            {
                _score.Value = 40;
            }
            else
            {
                _score.Value = 0;
            }
            _score.Used = true;
        }

        public void AddYahtzee(List<DieModel> _diceList)
        {
            var _score = ScoreList.Find(s => s.Name == "Yahtzee");
            if (VerifyScore.Yahtzee(_diceList))
            {
                if (_score.Used)
                {
                    _score.Value += 100;
                }
                else
                {
                    _score.Value = 50;
                }
            }
            else
            {
                _score.Value = 0;
            }
            _score.Used = true;
        }

        public void AddChance(List<DieModel> _diceList)
        {
            var _score = ScoreList.Find(s => s.Name == "Chance");
            foreach (DieModel _die in _diceList)
            {
                _score.Value += _die.Value;
            }
            _score.Used = true;
        }

        public int TotalScore()
        {
            var _total = 0;
            var _bonusCount = 0;
            foreach(Score _score in ScoreList)
            {
                if (_score.CountsBonus) _bonusCount += _score.Value;
                _total += _score.Value;
            }
            if (_bonusCount >= 63)
            {
                _total += 35;
            }

            return _total;
        }
    }
}
