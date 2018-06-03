using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfTesting.Models;

namespace WpfTesting.Infrastructure
{
    public class HistoryHelper
    {
        public static void AddHistory(MarketModel oldMarket, MarketModel newMarket, ObservableCollection<HistoryModel> listBox)
        {
            if (oldMarket is null || newMarket is null || newMarket.Selections is null || newMarket.Selections is null) return;
            //if (oldMarket.Name != newMarket.Name || oldMarket.MHandicap != newMarket.MHandicap || oldMarket.Selections[0].HandicapSign != newMarket.Selections[0].HandicapSign)
            //{
            //    list.Items.Clear();
            //    return;
            //}
            foreach (var currentMarketSelection in oldMarket.Selections)
            {
                var newSelection = newMarket.Selections.FirstOrDefault(x => x.Name == currentMarketSelection.Name);
                if (newSelection is null) /*listBox.Insert(0, $"{DateTime.Now:hh:mm:ss} - {currentMarketSelection.Name} - Suspended");*/
                    listBox.Insert(0, new HistoryModel() { Change = ChangeEnum.Suspended, SelectionName = currentMarketSelection.Name });
                else if (newSelection.Price > currentMarketSelection.Price)
                {
                    //listBox.Insert(0, $"{DateTime.Now:hh:mm:ss} - {newSelection.Name} - Up - {currentMarketSelection.Price} -> {newSelection.Price}");
                    listBox.Insert(0, new HistoryModel() { From = currentMarketSelection.Price, To = newSelection.Price, Change = ChangeEnum.Up, SelectionName = newSelection.Name });
                }
                else if (newSelection.Price < currentMarketSelection.Price)
                {
                    //listBox.Insert(0, $"{DateTime.Now:hh:mm:ss} - {newSelection.Name} - Down - {currentMarketSelection.Price} -> {newSelection.Price}");
                    listBox.Insert(0, new HistoryModel() { From = currentMarketSelection.Price, To = newSelection.Price, Change = ChangeEnum.Down, SelectionName = newSelection.Name });
                }
            }
            foreach (var newMarketSelection in newMarket.Selections)
            {
                var currentMarketSelection = oldMarket.Selections.FirstOrDefault(x => x.Name == newMarketSelection.Name);
                if (currentMarketSelection is null) /*listBox.Insert(0, $"{DateTime.Now:hh:mm:ss} - {newMarketSelection.Name} - Created - {newMarketSelection.Price}");*/
                    listBox.Insert(0, new HistoryModel() { SelectionName = newMarketSelection.Name, Change = ChangeEnum.Created });
            }
        }
    }
}
