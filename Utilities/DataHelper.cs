using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
using System.IO;
using Newtonsoft.Json;

namespace Yahtzee.Utilities
{
    class DataHelper
    {
        private static LiteDatabase GetLocalData()
        {
            return new LiteDatabase(DataDirectoryPath() + "yahtzee.data");
        }

        public static string DataDirectoryPath()
        {
            var _dataDir = $"{Environment.GetEnvironmentVariable("LocalAppData")}\\YahtzeeSharp\\";
            if (!Directory.Exists(_dataDir)) Directory.CreateDirectory(_dataDir);

            return _dataDir;
        }

        public static List<PlayerModel> GetLocalPlayers()
        {
            using (var db = GetLocalData())
            {
                var _playersTable = db.GetCollection<PlayerModel>("Players");
                return _playersTable.FindAll().ToList();
            }
        }


        public static void AddResults(ResultsModel _results)
        {
            using (var db = GetLocalData())
            {
                var _resultsTable = db.GetCollection<ResultsModel>("Results");
                _resultsTable.Insert(_results);
            }
        }

        public static List<ResultsModel> GetPastScores()
        {
            using (var db = GetLocalData())
            {
                var _resultsTable = db.GetCollection<ResultsModel>("Results");
                return _resultsTable.FindAll().ToList();
            }
        }

        public static void SaveLocalPlayers(List<PlayerModel> _players)
        {
            using (var db = GetLocalData())
            {
                var _playersTable = db.GetCollection<PlayerModel>("Players");
                foreach (PlayerModel _player in _playersTable.FindAll().ToList())
                {
                    _playersTable.Delete(_player.Id);
                }
                _playersTable.InsertBulk(_players);
            }
        }
    }
}
