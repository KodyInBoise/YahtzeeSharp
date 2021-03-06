﻿using System;
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
                new Score("3 of a Kind"),
                new Score("4 of a Kind"),
                new Score("Full House"),
                new Score("Small Straight"),
                new Score("Large Straight"),
                new Score("Yahtzee"),
                new Score("Chance"),
                new Score("Total")
            };
        }

        public List<Score> AvailableScores()
        {
            var _availableList = ScoreList.FindAll(x => x.Used == false && x.Name != "Total").ToList();
            var _yahtzee = ScoreList.Find(x => x.Name == "Yahtzee");

            return _availableList;
        }

        public void AddScore(Score _score, List<DieModel> _diceList)
        {
            switch (_score.Name)
            {
                case "Aces":
                    AddOnes(_diceList);
                    break;
                case "Twos":
                    AddTwos(_diceList);
                    break;
                case "Threes":
                    AddThrees(_diceList);
                    break;
                case "Fours":
                    AddFours(_diceList);
                    break;
                case "Fives":
                    AddFives(_diceList);
                    break;
                case "Sixes":
                    AddSixes(_diceList);
                    break;
                case "3 of a Kind":
                    AddThreeOfAKind(_diceList);
                    break;
                case "4 of a Kind":
                    AddFourOfAKind(_diceList);
                    break;
                case "Full House":
                    AddFullHouse(_diceList);
                    break;
                case "Small Straight":
                    AddSmallStraight(_diceList);
                    break;
                case "Large Straight":
                    AddLargeStraight(_diceList);
                    break;
                case "Yahtzee":
                    AddYahtzee(_diceList);
                    break;
                case "Chance":
                    AddChance(_diceList);
                    break;
            }
        }

        public void AddOnes(List<DieModel> _diceList)
        {
            var _score = ScoreList.Find(s => s.Name == "Aces");
            foreach (DieModel _die in _diceList)
            {
                if (_die.Value == 1)
                {
                    _score.Value += 1;
                }
            }
            _score.Used = true;
        }

        public void AddTwos(List<DieModel> _diceList)
        {
            var _score = ScoreList.Find(s => s.Name == "Twos");
            foreach (DieModel _die in _diceList)
            {
                if (_die.Value == 2)
                {
                    _score.Value += 2;
                }
            }
            _score.Used = true;
        }

        public void AddThrees(List<DieModel> _diceList)
        {
            var _score = ScoreList.Find(s => s.Name == "Threes");
            foreach (DieModel _die in _diceList)
            {
                if (_die.Value == 3)
                {
                    _score.Value += 3;
                }
            }
            _score.Used = true;
        }

        public void AddFours(List<DieModel> _diceList)
        {
            var _score = ScoreList.Find(s => s.Name == "Fours");
            foreach (DieModel _die in _diceList)
            {
                if (_die.Value == 4)
                {
                    _score.Value += 4;
                }
            }
            _score.Used = true;
        }

        public void AddFives(List<DieModel> _diceList)
        {
            var _score = ScoreList.Find(s => s.Name == "Fives");
            foreach (DieModel _die in _diceList)
            {
                if (_die.Value == 5)
                {
                    _score.Value += 5;
                }
            }
            _score.Used = true;
        }

        public void AddSixes(List<DieModel> _diceList)
        {
            var _score = ScoreList.Find(s => s.Name == "Sixes");
            foreach (DieModel _die in _diceList)
            {
                if (_die.Value == 6)
                {
                    _score.Value += 6;
                }
            }
            _score.Used = true;
        }

        public void AddThreeOfAKind(List<DieModel> _diceList)
        {
            var _score = ScoreList.Find(s => s.Name == "3 of a Kind");
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
            var _score = ScoreList.Find(s => s.Name == "4 of a Kind");
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
                if (_score.Name != "Total")
                {
                    if (_score.CountsBonus) _bonusCount += _score.Value;
                    _total += _score.Value;
                }
            }
            if (_bonusCount >= 63)
            {
                _total += 35;
            }

            var _totalScore = ScoreList.Find(x => x.Name == "Total");
            _totalScore.Value = _total;
            return _total;
        }
    }
}
