using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HtmlAgilityPack;
using Souq.ClassLib;
using Souq.ClassLib.Models;

namespace Souq.WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<LargeCategoryModel> LargeCategories { get; set; } = new List<LargeCategoryModel>();

        private static List<LargeCategoryModel> _selectedItems = new List<LargeCategoryModel>();

        public MainWindow()
        {
            InitializeComponent();
            StartBtn.IsEnabled = false;
            AddedLbl.Visibility = Visibility.Collapsed;
        }

        private void GetAllCategories(object sender, RoutedEventArgs e)
        {
            LargeCategories = Parser.GetLargeCategories();
            LargeList.ItemsSource = LargeCategories;
            CategoriesBtn.IsEnabled = false;
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            if (!(LargeList.SelectedItem is LargeCategoryModel selectedLarge)) return;
            var selectedMediums = MediumList.SelectedItems;
            var selectedSmalls = SmallList.SelectedItems;
            AddSelectedItems(selectedLarge, selectedMediums, selectedSmalls);

            AddedLbl.Visibility = Visibility.Visible;
            Thread.Sleep(2000);
            AddedLbl.Visibility = Visibility.Collapsed;
            if (_selectedItems.Any()) StartBtn.IsEnabled = true;
        }

        private void AddSelectedItems(LargeCategoryModel selectedLarge, IList selectedMediums, IList selectedSmalls)
        {
            if(selectedLarge is null) return;
            if(selectedMediums.Count ==0 && selectedSmalls.Count ==0) _selectedItems.Add(selectedLarge);
            else if(selectedMediums.Count != 0 && selectedSmalls.Count ==0) _selectedItems.Add(new LargeCategoryModel(){Name = selectedLarge.Name, MediumCategories = new List<MediumCategoryModel>(selectedMediums.OfType<MediumCategoryModel>()) });
            else if (selectedMediums.Count != 0 && selectedSmalls.Count != 0)
            {
                var smallBelongsMedium = selectedMediums.OfType<MediumCategoryModel>().FirstOrDefault(x=>x.SmallCategories.Any(y => y.Name == ((SmallCategoryModel)selectedSmalls[0]).Name));
                if (smallBelongsMedium is null)
                {
                    _selectedItems.Add(new LargeCategoryModel(){Name = selectedLarge.Name, MediumCategories = new List<MediumCategoryModel>(selectedMediums.OfType<MediumCategoryModel>()), SmallCategories = new List<SmallCategoryModel>(selectedSmalls.OfType<SmallCategoryModel>())});
                }
                else
                {
                    var mediums = selectedMediums.OfType<MediumCategoryModel>().Where(x=>x.Name != smallBelongsMedium.Name).ToList();
                    var selectedMedium = new MediumCategoryModel(){Name = smallBelongsMedium.Name, SmallCategories = new List<SmallCategoryModel>(selectedSmalls.OfType<SmallCategoryModel>())};
                    mediums.Add(selectedMedium);
                    var large2 = new LargeCategoryModel(){Name = selectedLarge.Name, MediumCategories = mediums};
                    _selectedItems.Add(large2);
                }
            }
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            if (_selectedItems.Any()) Parser.Start(_selectedItems);
            else MessageBox.Show("No category selected");
        }

        private void FillMediumsAndSmalls(object sender, RoutedEventArgs e)
        {
            if (!(LargeList.SelectedItem is LargeCategoryModel selectedLarge)) return;
            MediumSelectAll.IsChecked = false;
            MediumList.ItemsSource = selectedLarge.MediumCategories;
            SmallList.ItemsSource = selectedLarge.SmallCategories;
        }

        private void FillSmalls(object sender, RoutedEventArgs e)
        {
            SmallSelectAll.IsChecked = false;
            if (!(MediumList.SelectedItem is MediumCategoryModel selectedMedium)) return;
            SmallList.ItemsSource = selectedMedium.SmallCategories;
        }

        private void SelectAllSmall(object sender, RoutedEventArgs e)
        {
            SmallList.SelectAll();
        }

        private void UnSelectAllSmall(object sender, RoutedEventArgs e)
        {
            SmallList.UnselectAll();
        }

        private void SelectAllMedium(object sender, RoutedEventArgs e)
        {
            MediumList.SelectAll();
        }

        private void UnSelectAllMedium(object sender, RoutedEventArgs e)
        {
            MediumList.UnselectAll();
        }
    }
}
