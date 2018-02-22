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

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
            DiceList = DiceHelper.NewDiceSet();
            HeldDice = new List<DieModel>();
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
            await Task.Run(() => _helper.RollDice(DiceList));

            diceLB.Items.Clear();
            foreach (DieModel _DieModel in DiceList)
            {
                diceLB.Items.Add(_DieModel);
            }

            DisplayDiceImages(DiceList);

            CurrentGame.CurrentRoll++;
            rollTB.Text = $"Roll: {CurrentGame.CurrentRoll} of 3";
            if (CurrentGame.CurrentRoll == 3)
            {
                rollBTN.Visibility = Visibility.Collapsed;
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
            
            playerTB.Visibility = Visibility.Visible;
            rollTB.Visibility = Visibility.Visible;
            turnTB.Visibility = Visibility.Visible;

            startBTN.Visibility = Visibility.Collapsed;
        }

    }
}
