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
                diceOneIMG,
                diceTwoIMG,
                diceThreeIMG,
                diceFourIMG,
                diceFiveIMG,
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
        }
        
        private void LoadData()
        {
            var asdf = DataHelper.DataDirectoryPath();
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

            var _rollingDiceList = new List<DieModel>();
            foreach (DieModel _die in DiceList)
            {
                if (!HeldDiceList.Contains(_die))
                {
                    _rollingDiceList.Add(_die);
                }
            }
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
                diceOneIMG, 
                diceTwoIMG,
                diceThreeIMG, 
                diceFourIMG,
                diceFiveIMG
            };

            var x = 0;
            foreach (DieModel _die in _diceList)
            {
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

        private void StartNewGame()
        {
            CurrentGame = new GameTracker();
            ActivePlayer = new PlayerModel
            {
                Name = "Kody",
                Scorecard = new ScoreTracker()
            };
            TurnList = new List<TurnModel>();
            ActiveTurn = new TurnModel
            {
                Player = ActivePlayer,
                StartTime = DateTime.Today.TimeOfDay.ToString(),
            };
            ResetHeldDice();           

            playerTB.Visibility = Visibility.Visible;
            rollTB.Visibility = Visibility.Visible;
            turnTB.Visibility = Visibility.Visible;
            rollBTN.Visibility = Visibility.Visible;

            startBTN.Visibility = Visibility.Collapsed;
        }

        private void StartNextTurn()
        {
            ActiveTurn.EndTime = DateTime.Today.TimeOfDay.ToString();
            TurnList.Add(ActiveTurn);

            ActiveTurn = new TurnModel
            {
                StartTime = DateTime.Today.TimeOfDay.ToString(),
                Player = ActivePlayer,
            };
            DiceList = DiceHelper.NewDiceSet();
            ResetHeldDice();

            turnTB.Text = $"Turn: {TurnList.Count + 1}";
            nextTurnBTN.Visibility = Visibility.Collapsed;
            rollBTN.Visibility = Visibility.Visible;
        }

        private void nextTurnBTN_Click(object sender, RoutedEventArgs e)
        {
            StartNextTurn();
        }

        private void HoldDie(DieModel _die, ImageSource _imageSource)
        {
            var _heldCount = HeldDiceList.Count;
            var _newHeldIMG = HeldDiceImages[_heldCount];
            _newHeldIMG.Source = _imageSource;
            _newHeldIMG.Visibility = Visibility.Visible;
            HeldDiceList.Add(_die);
        }

        private void diceOneIMG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HoldDie(DiceList[0], diceOneIMG.Source);
            OpenDiceImages.Add(diceOneIMG);
            diceOneIMG.Visibility = Visibility.Collapsed;
        }

        private void diceTwoIMG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HoldDie(DiceList[1], diceTwoIMG.Source);
            OpenDiceImages.Add(diceTwoIMG);
            diceTwoIMG.Visibility = Visibility.Collapsed;
        }

        private void diceThreeIMG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HoldDie(DiceList[2], diceThreeIMG.Source);
            OpenDiceImages.Add(diceThreeIMG);
            diceThreeIMG.Visibility = Visibility.Collapsed;
        }

        private void diceFourIMG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HoldDie(DiceList[3], diceFourIMG.Source);
            OpenDiceImages.Add(diceFourIMG);
            diceFourIMG.Visibility = Visibility.Collapsed;
        }

        private void diceFiveIMG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HoldDie(DiceList[4], diceFiveIMG.Source);
            OpenDiceImages.Add(diceFiveIMG);
            diceFiveIMG.Visibility = Visibility.Collapsed;
        }

        private void ResetHeldDice()
        {
            foreach (Image _image in HeldDiceImages)
            {
                _image.Visibility = Visibility.Collapsed;
            }
            HeldDiceList = new List<DieModel>();
        }

        private void AddToRoll(DieModel _die, ImageSource _imageSource)
        {
            Image _newDiceIMG;
            if (OpenDiceImages.Count > 0)
            {
                _newDiceIMG = OpenDiceImages[0];
                OpenDiceImages.RemoveAt(0);
            }
            else
            {
                _newDiceIMG = DiceImages[DiceList.Count - HeldDiceList.Count];
            }
            _newDiceIMG.Source = _imageSource;
            _newDiceIMG.Visibility = Visibility.Visible;
            DiceList.Add(_die);
            HeldDiceList.Remove(_die);
        }

        private void heldOneIMG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AddToRoll(HeldDiceList[0], heldOneIMG.Source);
            heldOneIMG.Visibility = Visibility.Collapsed;
        }

        private void heldTwoIMG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AddToRoll(HeldDiceList[1], heldTwoIMG.Source);
            heldTwoIMG.Visibility = Visibility.Collapsed;
        }

        private void heldThreeIMG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AddToRoll(HeldDiceList[2], heldThreeIMG.Source);
            heldThreeIMG.Visibility = Visibility.Collapsed;
        }

        private void heldFourIMG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AddToRoll(HeldDiceList[3], heldFourIMG.Source);
            heldFourIMG.Visibility = Visibility.Collapsed;
        }

        private void heldFiveIMG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AddToRoll(HeldDiceList[4], heldFiveIMG.Source);
            heldFiveIMG.Visibility = Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var _allDice = new List<DieModel>();
            _allDice.AddRange(DiceList);
            foreach (DieModel _die in HeldDiceList)
            {
                if (!_allDice.Contains(_die))
                {

                    _allDice.Add(_die);
                }
            }
            ActivePlayer.Scorecard.AddChance(_allDice);
        }
    }
}
