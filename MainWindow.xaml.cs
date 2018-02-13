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
            DiceList = DiceHelper.NewDiceSet();
            HeldDice = new List<DieModel>();
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

            CurrentGame.CurrentRoll++;
            rollTB.Text = $"Roll: {CurrentGame.CurrentRoll} of 3";
            if (CurrentGame.CurrentRoll == 3)
            {
                rollBTN.Visibility = Visibility.Collapsed;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //ActivePlayer.Scorecard.AddScore(HeldDice, 2);
            var _scoreWindow = new ScoreWindow(this);
            _scoreWindow.Show();
        }
    }
}
