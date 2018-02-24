using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yahtzee.Scorecard;
using Yahtzee.ScoreHelper;

namespace Yahtzee.Utilities
{
    public class GameTracker
    { 
        public string StartedTimestamp { get; set; }
        public string EndedTimestamp { get; set; }
        public List<PlayerModel> Players { get; set; }
        public List<TurnModel> TurnList { get; set; }
        public int Turns { get; set; }
        public string Winner { get; set; }

        public PlayerModel CurrentPlayer { get; set; }
        public int CurrentRoll { get; set; }

        public GameTracker()
        {
            StartedTimestamp = DateTime.Now.ToString();
            Players = new List<PlayerModel>();
            TurnList = new List<TurnModel>();
        }

        public void GetResults()
        {

        }

        public List<ScoreResult> GetScoreResultList()
        {
            var _scoreResults = new List<ScoreResult>();
            var x = 0;
            foreach (Score _score in Players[0].Scorecard.ScoreList)
            {
                var _newResult = new ScoreResult
                {
                    Name = _score.Name,
                };
                try
                {
                    if (Players[0] != null) _newResult.PlayerOne = Players[0].Scorecard.ScoreList[x].Value;
                    if (Players[1] != null) _newResult.PlayerTwo = Players[1].Scorecard.ScoreList[x].Value;
                    if (Players[2] != null) _newResult.PlayerThree = Players[2].Scorecard.ScoreList[x].Value;
                    if (Players[3] != null) _newResult.PlayerFour = Players[3].Scorecard.ScoreList[x].Value;
                    if (Players[4] != null) _newResult.PlayerFive = Players[4].Scorecard.ScoreList[x].Value;
                }
                catch(ArgumentOutOfRangeException)
                {
                    
                }
                finally
                {
                    _scoreResults.Add(_newResult);
                    x++;
                }

            }

            return _scoreResults;
        }
    }
}
