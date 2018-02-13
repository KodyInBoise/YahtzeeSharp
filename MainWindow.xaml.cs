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
using Yahtzee.Utilities;

namespace Yahtzee
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
    }
}
