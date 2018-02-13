using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee.Utilities
{
    public class DiceHelper
    {
        public static List<DieModel> NewDiceSet()
        {
            var _diceList = new List<DieModel>();
            var _diceCount = 0;   

            while (_diceCount < 5)
            {
                var _newDieModel = new DieModel
                {
                    Id = _diceCount + 1
                };
                _diceList.Add(_newDieModel);
                _diceCount++;
            }

            return _diceList;
        }

        public async Task RollDice(List<DieModel> _diceList)
        {
            Random _roller = new Random();
            foreach (DieModel _DieModel in _diceList)
            {
                _DieModel.Value = _roller.Next(1, 7);
            }
        }
    }
}
