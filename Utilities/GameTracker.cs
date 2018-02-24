using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private class PlayerResults
        {
            string ScoreName { get; set; }

        }
    }
}
