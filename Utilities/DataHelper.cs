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

        public static void AddResult(ResultsModel _results)
        {
            using (var db = GetLocalData())
            {
                var _resultsTable = db.GetCollection<ResultsModel>("Results");
                _resultsTable.Insert(_results);
            }
        }

        public static PlayerModel GetLocalPlayer()
        {
            var _playerDataPath = DataDirectoryPath() + "player.json";
            if (File.Exists(_playerDataPath))
            {
                return JsonConvert.DeserializeObject<PlayerModel>(_playerDataPath);
            }

            return null;
        }

        public static void SaveLocalPlayer(PlayerModel _player)
        {
            var _playerDataPath = DataDirectoryPath() + "player.json";
            File.WriteAllText(_playerDataPath, JsonConvert.SerializeObject(_player));
        }
    }
}
