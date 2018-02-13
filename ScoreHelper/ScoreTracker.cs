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
            ScoreList = new List<Score>();
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
    }
}
