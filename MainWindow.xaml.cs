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
        public List<DieModel> HeldDice;
        private List<Image> HeldDiceImages;
        private TurnModel ActiveTurn;
        private List<TurnModel> TurnList;

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
            DiceList = DiceHelper.NewDiceSet();
            HeldDice = new List<DieModel>();

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
            foreach (DieModel _die in HeldDice)
            {
                DiceList.Remove(_die);
            }

            var _helper = new DiceHelper();
            await Task.Run(() => _helper.RollDice(DiceList));

            diceLB.Items.Clear();
            foreach (DieModel _DieModel in DiceList)
            {
                diceLB.Items.Add(_DieModel);
            }

            DisplayDiceImages(DiceList);

            ActiveTurn.RollCount++;
            rollTB.Text = $"Roll: {ActiveTurn.RollCount} of 3";
            if (ActiveTurn.RollCount == 3)
            {
                rollBTN.Visibility = Visibility.Collapsed;
                nextTurnBTN.Visibility = Visibility.Visible;
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

        private void holdBTN_Click(object sender, RoutedEventArgs e)
        {
            var _selectedDieModel = (DieModel) diceLB.SelectedItem;
            DiceList.Remove(_selectedDieModel);
            HeldDice.Add(_selectedDieModel);
            diceLB.Items.Remove(_selectedDieModel);
            heldLB.Items.Add(_selectedDieModel);
        }

        private void unholdBTN_Click(object sender, RoutedEventArgs e)
        {
            var _selectedDieModel = (DieModel) heldLB.SelectedItem;
            HeldDice.Remove(_selectedDieModel);
            DiceList.Add(_selectedDieModel);
            heldLB.Items.Remove(_selectedDieModel);
            diceLB.Items.Add(_selectedDieModel);
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

        private void HoldDie(int _index, ImageSource _imageSource)
        {
            var _heldCount = HeldDice.Count;
            var _newHeldIMG = HeldDiceImages[_heldCount];
            _newHeldIMG.Source = _imageSource;
            _newHeldIMG.Visibility = Visibility.Visible;
            HeldDice.Add(DiceList[_index]);
            heldLB.Items.Add(DiceList[_index]);

        }

        private void diceOneIMG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HoldDie(0, diceOneIMG.Source);
            diceOneIMG.Visibility = Visibility.Collapsed;
        }

        private void diceTwoIMG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HoldDie(1, diceTwoIMG.Source);
            diceTwoIMG.Visibility = Visibility.Collapsed;
        }

        private void diceThreeIMG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HoldDie(2, diceThreeIMG.Source);
            diceThreeIMG.Visibility = Visibility.Collapsed;
        }

        private void diceFourIMG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HoldDie(3, diceFourIMG.Source);
            diceFourIMG.Visibility = Visibility.Collapsed;
        }

        private void diceFiveIMG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HoldDie(4, diceFiveIMG.Source);
            diceFiveIMG.Visibility = Visibility.Collapsed;
        }

        private void ResetHeldDice()
        {
            foreach (Image _image in HeldDiceImages)
            {
                _image.Visibility = Visibility.Collapsed;
            }
            HeldDice = new List<DieModel>();
        }
    }
}
