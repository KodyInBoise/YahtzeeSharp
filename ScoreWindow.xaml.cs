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
using System.Windows.Shapes;
using Yahtzee.Scorecard;

namespace Yahtzee
{
    /// <summary>
    /// Interaction logic for ScoreWindow.xaml
    /// </summary>
    public partial class ScoreWindow : Window
    {
        public List<Score> ScoreList;
        private MainWindow MainWin;

        public ScoreWindow(MainWindow _mainWin)
        {
            InitializeComponent();
            MainWin = _mainWin;
            this.Left = _mainWin.Left + MainWin.Width;
            this.Top = _mainWin.Top;

            ShowScores();
        }

        public void ShowScores()
        {
            ScoreList = new List<Score>();

            var ones = new Score { Name = "1's", Value = 0}; ScoreList.Add(ones);
            var twos = new Score { Name = "2's", Value = 0 }; ScoreList.Add(twos);
            var threes = new Score { Name = "3's", Value = 0 }; ScoreList.Add(threes);
            var fours = new Score { Name = "4's", Value = 0 }; ScoreList.Add(fours);
            var fives = new Score { Name = "5's", Value = 0 }; ScoreList.Add(fives);
            var sixes = new Score { Name = "6's", Value = 0 }; ScoreList.Add(sixes);

            CollectionViewSource itemCollectionViewSource;
            itemCollectionViewSource = (CollectionViewSource)(FindResource("ItemCollectionViewSource"));
            itemCollectionViewSource.Source = ScoreList;
        }
    }
}
