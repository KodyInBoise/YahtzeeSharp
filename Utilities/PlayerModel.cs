using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yahtzee.Scorecard;

namespace Yahtzee.Utilities
{
    public class PlayerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [LiteDB.BsonIgnore]
        public ScoreTracker Scorecard { get; set; }

        public PlayerModel()
        {
            Scorecard = new ScoreTracker();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
