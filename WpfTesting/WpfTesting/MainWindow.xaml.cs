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
using WpfTesting.Models;

namespace WpfTesting
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window // buniatyan ashot, haykn a asel, 2rd hark, 2rd hivandanoc
    {
        public MainWindow()
        {
            InitializeComponent();
            //var market1 = new MarketModel() { Name ="a", MHandicap=9.50, Selections = new List<SelectionModel>() { new SelectionModel() { HandicapSign=-1}, new SelectionModel() } };
            //var market2 = new MarketModel() { Name = "a", MHandicap = 9.5, Selections = new List<SelectionModel>() { new SelectionModel() { HandicapSign = 1 }, new SelectionModel() } };
            //var match1 = new MatchModel() { CompetitionName = "a", MatchMembers = new List<MatchMemberModel>() { new MatchMemberModel() { Name="1"}, new MatchMemberModel() { Name="3"} } };
            //var match2 = new MatchModel() { CompetitionName="a", MatchMembers = new List<MatchMemberModel>() { new MatchMemberModel() { Name="2"}, new MatchMemberModel() { Name="1"} } };
            //MessageBox.Show((match1 == match2).ToString());
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
