using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Yahtzee.Scorecard;
using Yahtzee.Utilities;
using System.IO;

namespace Yahtzee
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public PlayerModel ActivePlayer;
        public GameTracker CurrentGame;
        public List<DieModel> DiceList;
        private List<Image> DiceImages;
        private List<Image> OpenDiceImages;
        public List<DieModel> HeldDiceList;
        private List<Image> HeldDiceImages;
        private List<Image> OpenHeldImages;
        private TurnModel ActiveTurn;
        private List<TurnModel> TurnList;
        public List<Score> PlayerScore;
        
        Dictionary<Image, int> RollingImageDictionary;
        Dictionary<Image, int> HeldImageDictionary;

        public MainWindow()
        {
            InitializeComponent();
            LoadData();

            DiceList = DiceHelper.NewDiceSet();
            HeldDiceList = new List<DieModel>();
            OpenDiceImages = new List<Image>();
            OpenHeldImages = new List<Image>();

            DiceImages = new List<Image>
            {
                rollingOneIMG,
                rollingTwoIMG,
                rollingThreeIMG,
                rollingFourIMG,
                rollingFiveIMG,
            };
            HeldDiceImages = new List<Image>
            {
                heldOneIMG,
                heldTwoIMG,
                heldThreeIMG,
                heldFourIMG,
                heldFiveIMG,
            };

            rollBTN.Visibility = Visibility.Collapsed;
            CurrentGame = CreateNewGame();
            ShowNewGame();
        }

        private Dictionary<Image, int> CreateRollingDictionary()
        {
            //Create dictionary with Image and dice id if showing one, 0 if open
            var _dictionary = new Dictionary<Image, int>();
            _dictionary.Add(rollingOneIMG, 0);
            _dictionary.Add(rollingTwoIMG, 0);
            _dictionary.Add(rollingThreeIMG, 0);
            _dictionary.Add(rollingFourIMG, 0);
            _dictionary.Add(rollingFiveIMG, 0);

            return _dictionary;
        }

        private Dictionary<Image, int> CreateHeldDictionary()
        {
            //Create dictionary with Image and dice id if showing one, 0 if open
            var _dictionary = new Dictionary<Image, int>();
            _dictionary.Add(heldOneIMG, 0);
            _dictionary.Add(heldTwoIMG, 0);
            _dictionary.Add(heldThreeIMG, 0);
            _dictionary.Add(heldFourIMG, 0);
            _dictionary.Add(heldFiveIMG, 0);

            return _dictionary;
        }

        private void LoadData()
        {
            var _data = DataHelper.DataDirectoryPath();
        }

        private void rollBTN_Click(object sender, RoutedEventArgs e)
        {
            RollDice();
        }

        private async void RollDice()
        {
            var _helper = new DiceHelper();
            OpenDiceImages = new List<Image>();
            OpenHeldImages = new List<Image>();

            var _rollingDiceList = DiceList.FindAll(x => x.IsHeld == false);
            await Task.Run(() => _helper.RollDice(_rollingDiceList));

            DisplayDiceImages(_rollingDiceList);

            ActiveTurn.RollCount++;
            rollTB.Text = $"Roll: {ActiveTurn.RollCount} of 3";
            if (ActiveTurn.RollCount == 3)
            {
                //rollBTN.Visibility = Visibility.Collapsed;
                //nextTurnBTN.Visibility = Visibility.Visible;
            }
        }

        private void DisplayDiceImages(List<DieModel> _diceList)
        {
            var _imageList = new List<Image>
            {
                rollingOneIMG, 
                rollingTwoIMG,
                rollingThreeIMG, 
                rollingFourIMG,
                rollingFiveIMG
            };

            var x = 0;
            foreach (DieModel _die in _diceList)
            {
                RollingImageDictionary[_imageList[x]] = _die.Id;
                _imageList[x].Visibility = Visibility.Visible;
                switch(_die.Value)
                {
                    case 1:
                        _imageList[x].Source = ImageHelper.DiceOne();
                        break;
                    case 2:
                        _imageList[x].Source = ImageHelper.DiceTwo();
                        break;
                    case 3:
                        _imageList[x].Source = ImageHelper.DiceThree();
                        break;
                    case 4:
                        _imageList[x].Source = ImageHelper.DiceFour();
                        break;
                    case 5:
                        _imageList[x].Source = ImageHelper.DiceFive();
                        break;
                    case 6:
                        _imageList[x].Source = ImageHelper.DiceSix();
                        break;
                }
                x++;
            }

            if (_diceList.Count < 5)
            {
                var i = 4;
                while (i > _diceList.Count - 1)
                {
                    RollingImageDictionary[_imageList[i]] = 0;
                    _imageList[i].Visibility = Visibility.Collapsed;
                    i--;
                }
            }
        }

        private void unholdBTN_Click(object sender, RoutedEventArgs e)
        {
            //HeldDice.Remove(_selectedDieModel);
            //DiceList.Add(_selectedDieModel);
        }

        private void startBTN_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame();
        }

        private GameTracker CreateNewGame()
        {
            CurrentGame = new GameTracker();
            return CurrentGame;
        }

        private TurnModel NewTurn()
        {
            var _newTurn = new TurnModel
            {
                StartTime = DateTime.Now.TimeOfDay.ToString(),
                Player = ActivePlayer,
            };

            return _newTurn;
        }

        private void StartNewGame()
        {
            ActivePlayer = CurrentGame.Players[0];
            StartNextTurn();
            /*
            CurrentGame = new GameTracker();
            if (CurrentGame.Players.Count == 0)
            {
                ActivePlayer = new PlayerModel
                {
                    Name = "Kody",
                    Scorecard = new ScoreTracker()
                };
                CurrentGame.Players.Add(ActivePlayer);
            }

            ActiveTurn = new TurnModel
            {
                Player = ActivePlayer,
                StartTime = DateTime.Today.TimeOfDay.ToString(),
            };
            RollingImageDictionary = CreateRollingDictionary();
            HeldImageDictionary = CreateHeldDictionary();
            ResetHeldDice();

            availableLB.ItemsSource = ActivePlayer.Scorecard.AvailableScores();

            playerTB.Visibility = Visibility.Visible;
            rollTB.Visibility = Visibility.Visible;
            turnTB.Visibility = Visibility.Visible;
            rollBTN.Visibility = Visibility.Visible;

            startBTN.Visibility = Visibility.Collapsed;
            */
        }

        private void StartNextTurn()
        {
            if (ActiveTurn == null)
            {
                ActiveTurn = NewTurn();
            }
            else
            {
                ActiveTurn.EndTime = DateTime.Today.TimeOfDay.ToString();
                CurrentGame.TurnList.Add(ActiveTurn);
                var _activeIndex = CurrentGame.Players.IndexOf(ActivePlayer);
                if (_activeIndex == CurrentGame.Players.Count - 1)
                {
                    ActivePlayer = CurrentGame.Players[0];
                }
                else
                {
                    ActivePlayer = CurrentGame.Players[_activeIndex + 1];
                }

                ActiveTurn = NewTurn();
            }

            DiceList = DiceHelper.NewDiceSet();
            RollingImageDictionary = CreateRollingDictionary();
            HeldImageDictionary = CreateHeldDictionary();

            playerTB.Text = $"Player: {ActivePlayer.Name}";
            turnTB.Text = $"Turn: {CurrentGame.TurnList.Count + 1}";
            rollTB.Text = $"Roll: 1 of 3";
            scoreTB.Text = $"Score: {ActivePlayer.Scorecard.TotalScore()}";

            availableLB.ItemsSource = null;
            availableLB.ItemsSource = ActivePlayer.Scorecard.AvailableScores();

            scorePointsTB.Visibility = Visibility.Collapsed;
            scoreNameTB.Visibility = Visibility.Collapsed;
            heldOneIMG.Visibility = Visibility.Collapsed;
            heldTwoIMG.Visibility = Visibility.Collapsed;
            heldThreeIMG.Visibility = Visibility.Collapsed;
            heldFourIMG.Visibility = Visibility.Collapsed;
            heldFiveIMG.Visibility = Visibility.Collapsed;
            nextTurnBTN.Visibility = Visibility.Collapsed;
            rollBTN.Visibility = Visibility.Visible;
            UseScoreBTN.Visibility = Visibility.Visible;
        }

        private void nextTurnBTN_Click(object sender, RoutedEventArgs e)
        {
            StartNextTurn();
        }

        private void HoldDie(DieModel _heldDie)
        {
            _heldDie.IsHeld = true;
            var _heldImage = HeldImageDictionary.First(x => x.Value == 0).Key;
            switch(_heldDie.Value)
            {
                case 1:
                    _heldImage.Source = ImageHelper.DiceOne();
                    break;
                case 2:
                    _heldImage.Source = ImageHelper.DiceTwo();
                    break;
                case 3:
                    _heldImage.Source = ImageHelper.DiceThree();
                    break;
                case 4:
                    _heldImage.Source = ImageHelper.DiceFour();
                    break;
                case 5:
                    _heldImage.Source = ImageHelper.DiceFive();
                    break;
                case 6:
                    _heldImage.Source = ImageHelper.DiceSix();
                    break;
            }

            HeldImageDictionary[_heldImage] = _heldDie.Id;
            _heldImage.Visibility = Visibility.Visible;
        }

        private void diceOneIMG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DieModel _heldDie = (DieModel)DiceList.Find(x => x.Id == RollingImageDictionary[rollingOneIMG]);
            HoldDie(_heldDie);

            RollingImageDictionary[rollingOneIMG] = 0;
            rollingOneIMG.Visibility = Visibility.Collapsed;
        }

        private void diceTwoIMG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DieModel _heldDie = (DieModel)DiceList.Find(x => x.Id == RollingImageDictionary[rollingTwoIMG]);
            HoldDie(_heldDie);

            RollingImageDictionary[rollingTwoIMG] = 0;
            rollingTwoIMG.Visibility = Visibility.Collapsed;
        }

        private void diceThreeIMG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DieModel _heldDie = (DieModel)DiceList.Find(x => x.Id == RollingImageDictionary[rollingThreeIMG]);
            HoldDie(_heldDie);

            RollingImageDictionary[rollingThreeIMG] = 0;
            rollingThreeIMG.Visibility = Visibility.Collapsed;
        }

        private void diceFourIMG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DieModel _heldDie = (DieModel)DiceList.Find(x => x.Id == RollingImageDictionary[rollingFourIMG]);
            HoldDie(_heldDie);

            RollingImageDictionary[rollingFourIMG] = 0;
            rollingFourIMG.Visibility = Visibility.Collapsed;
        }

        private void diceFiveIMG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DieModel _heldDie = (DieModel)DiceList.Find(x => x.Id == RollingImageDictionary[rollingFiveIMG]);
            HoldDie(_heldDie);

            RollingImageDictionary[rollingFiveIMG] = 0;
            rollingFiveIMG.Visibility = Visibility.Collapsed;
        }

        private void AddToRoll(DieModel _rollingDie)
        {
            _rollingDie.IsHeld = false;
            var _rollingIMG = RollingImageDictionary.First(x => x.Value == 0).Key;
            switch (_rollingDie.Value)
            {
                case 1:
                    _rollingIMG.Source = ImageHelper.DiceOne();
                    break;
                case 2:
                    _rollingIMG.Source = ImageHelper.DiceTwo();
                    break;
                case 3:
                    _rollingIMG.Source = ImageHelper.DiceThree();
                    break;
                case 4:
                    _rollingIMG.Source = ImageHelper.DiceFour();
                    break;
                case 5:
                    _rollingIMG.Source = ImageHelper.DiceFive();
                    break;
                case 6:
                    _rollingIMG.Source = ImageHelper.DiceSix();
                    break;
            }
            RollingImageDictionary[_rollingIMG] = _rollingDie.Id;
            _rollingIMG.Visibility = Visibility.Visible;
        }

        private void heldOneIMG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DieModel _rollingDie = (DieModel)DiceList.Find(x => x.Id == HeldImageDictionary[heldOneIMG]);
            AddToRoll(_rollingDie);

            HeldImageDictionary[heldOneIMG] = 0;
            heldOneIMG.Visibility = Visibility.Collapsed;
        }

        private void heldTwoIMG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DieModel _rollingDie = (DieModel)DiceList.Find(x => x.Id == HeldImageDictionary[heldTwoIMG]);
            AddToRoll(_rollingDie);

            HeldImageDictionary[heldTwoIMG] = 0;
            heldTwoIMG.Visibility = Visibility.Collapsed;
        }

        private void heldThreeIMG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DieModel _rollingDie = (DieModel)DiceList.Find(x => x.Id == HeldImageDictionary[heldThreeIMG]);
            AddToRoll(_rollingDie);

            HeldImageDictionary[heldThreeIMG] = 0;
            heldThreeIMG.Visibility = Visibility.Collapsed;
        }

        private void heldFourIMG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DieModel _rollingDie = (DieModel)DiceList.Find(x => x.Id == HeldImageDictionary[heldFourIMG]);
            AddToRoll(_rollingDie);

            HeldImageDictionary[heldFourIMG] = 0;
            heldFourIMG.Visibility = Visibility.Collapsed;
        }

        private void heldFiveIMG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DieModel _rollingDie = (DieModel)DiceList.Find(x => x.Id == HeldImageDictionary[heldFiveIMG]);
            AddToRoll(_rollingDie);

            HeldImageDictionary[heldFiveIMG] = 0;
            heldFiveIMG.Visibility = Visibility.Collapsed;
        }

        private void DisplayScores()
        {
            try
            {
                ScoreGrid.ItemsSource = null;
                ScoreGrid.ItemsSource = ActivePlayer.Scorecard.ScoreList;

                if (ScoreGrid.Columns.Count > 2)
                {
                    ScoreGrid.Columns.RemoveAt(2);
                    ScoreGrid.Columns.RemoveAt(2);

                    ScoreGrid.Columns[0].Header = string.Empty;
                    ScoreGrid.Columns[1].Header = ActivePlayer.Name;
                }
            }
            catch
            {
                ScoreGrid.ItemsSource = null;
            }
        }

        private void scoreTAB_GotFocus(object sender, RoutedEventArgs e)
        {
            DisplayScores();
        }

        private void UseScoreBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var _scoreName = availableLB.SelectedValue.ToString();
                var _score = ActivePlayer.Scorecard.ScoreList.Find(x => x.Name == _scoreName);
                AddSelectedScore(_score);

                availableLB.ItemsSource = ActivePlayer.Scorecard.AvailableScores();
                UseScoreBTN.Visibility = Visibility.Collapsed;
                rollBTN.Visibility = Visibility.Collapsed;
                rollingOneIMG.Visibility = Visibility.Collapsed;
                rollingTwoIMG.Visibility = Visibility.Collapsed;
                rollingThreeIMG.Visibility = Visibility.Collapsed;
                rollingFourIMG.Visibility = Visibility.Collapsed;
                rollingFiveIMG.Visibility = Visibility.Collapsed;


                scorePointsTB.Text = $"{_score.Value} points";
                scoreNameTB.Text = $"{_score.Name}";
                scorePointsTB.Visibility = Visibility.Visible;
                scoreNameTB.Visibility = Visibility.Visible;
                nextTurnBTN.Visibility = Visibility.Visible;
            }
            catch
            {

            }
        }

        private void AddSelectedScore(Score _score)
        {
            if (!_score.Used)
            {
                ActivePlayer.Scorecard.AddScore(_score, DiceList);
                scoreTB.Text = $"Score: {ActivePlayer.Scorecard.TotalScore()}";
            }
            else
            {
                //availableLB.Items.Remove(availableLB.SelectedItem);
            }
        }

        private void addPlayerBTN_Click(object sender, RoutedEventArgs e)
        {
            AddPlayer();
        }

        private void AddPlayer()
        {
            var _player = CurrentGame.Players.Find(x => x.Name == addPlayerTB.Text);
            if (_player == null)
            {
                _player = new PlayerModel
                {
                    Name = addPlayerTB.Text
                };
                CurrentGame.Players.Add(_player);
                playersLB.Items.Refresh();
                addPlayerTB.Text = "Player Name";
            }
            addPlayerTB.SelectAll();

            DataHelper.SaveLocalPlayer(_player);
        }

        private void settingsTAB_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                playersLB.ItemsSource = CurrentGame.Players;
            }
            catch
            {

            }
        }

        private void startGameBTN_Click(object sender, RoutedEventArgs e)
        {
            MainTC.SelectedIndex = 1;
            StartNewGame();
        }

        private void ShowNewGame()
        {
            addPlayerTB.Visibility = Visibility.Visible;
            addPlayerTB.Visibility = Visibility.Visible;
            playersLB.Visibility = Visibility.Visible;
            startGameBTN.Visibility = Visibility.Visible;

            heldOneIMG.Visibility = Visibility.Collapsed;
            heldTwoIMG.Visibility = Visibility.Collapsed;
            heldThreeIMG.Visibility = Visibility.Collapsed;
            heldFourIMG.Visibility = Visibility.Collapsed;
            heldFiveIMG.Visibility = Visibility.Collapsed;
        }

        private void ShowActiveGame()
        {
            addPlayerTB.Visibility = Visibility.Collapsed;
            addPlayerTB.Visibility = Visibility.Collapsed;
            playersLB.Visibility = Visibility.Collapsed;
            startGameBTN.Visibility = Visibility.Collapsed;
        }

        private void HideActiveGame()
        {
            playerTB.Visibility = Visibility.Collapsed;
            turnTB.Visibility = Visibility.Collapsed;
            rollTB.Visibility = Visibility.Collapsed;
            scoreTB.Visibility = Visibility.Collapsed;
        }

        private void addPlayerTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddPlayer();
            }
        }

        private void FinishGame()
        {

        }
    }
}
