using System;
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
        }
        
        private void GetAllCategories(object sender, RoutedEventArgs e)
        {
            LargeCategories = Parser.GetLargeCategories();
            LargeList.ItemsSource = LargeCategories;
            CategoriesBtn.IsEnabled = false;
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            var selectedLarge = LargeList.SelectedItem as LargeCategoryModel;
            if(selectedLarge is null) return;
            var selectedMedium = MediumList.SelectedItems as List<MediumCategoryModel>;
            var selectedSmall = SmallList.SelectedItems as List<SmallCategoryModel>;
            var alreadyExistingLarge = _selectedItems.FirstOrDefault(x => x.Name == selectedLarge.Name);
            if(alreadyExistingLarge is null && selectedSmall is null && selectedMedium is null) _selectedItems.Add(selectedLarge);
            if (selectedSmall is null && selectedMedium != null && selectedMedium.Any())
            {
                if(alreadyExistingLarge != null) alreadyExistingLarge.MediumCategories.AddRange(selectedMedium);
                else _selectedItems.Add(new LargeCategoryModel(){Name = selectedLarge.Name, MediumCategories = selectedMedium});
            }

            AddedLbl.Visibility = Visibility.Visible;
            Thread.Sleep(2000);
            AddedLbl.Visibility = Visibility.Collapsed;
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            //Parser.Start(_selectedItems);
        }

        private void FillMediumsAndSmalls(object sender, RoutedEventArgs e)
        {
            if(!(LargeList.SelectedItem is LargeCategoryModel selectedLarge)) return;
            MediumList.ItemsSource = selectedLarge.MediumCategories;
            SmallList.ItemsSource = selectedLarge.SmallCategories;
        }

        private void FillSmalls(object sender, RoutedEventArgs e)
        {
            if(!(MediumList.SelectedItem is MediumCategoryModel selectedMedium)) return;
            SmallList.ItemsSource = selectedMedium.SmallCategories;
        }
    }
}
