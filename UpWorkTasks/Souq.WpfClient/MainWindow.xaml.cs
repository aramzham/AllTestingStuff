using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HtmlAgilityPack;
using Souq.ClassLib;
using Souq.ClassLib.Models;
using MessageBox = System.Windows.MessageBox;

namespace Souq.WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<LargeCategoryModel> LargeCategories { get; set; } = new List<LargeCategoryModel>();

        private static List<LargeCategoryModel> _selectedItems = new List<LargeCategoryModel>();
        private static string _path;

        public MainWindow()
        {
            InitializeComponent();
            StartBtn.IsEnabled = false;
            //AddedLbl.Visibility = Visibility.Collapsed;
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
            if (_selectedItems.Any())
            {
                StartBtn.IsEnabled = true;
                MessageBox.Show("Added");
            }
        }

        private void AddSelectedItems(LargeCategoryModel selectedLarge, IList selectedMediums, IList selectedSmalls)
        {
            if (selectedLarge is null) return;
            if (selectedMediums.Count == 0 && selectedSmalls.Count == 0) _selectedItems.Add(selectedLarge);
            else if (selectedMediums.Count == 0 && selectedSmalls.Count != 0) _selectedItems.Add(new LargeCategoryModel() { Name = selectedLarge.Name, SmallCategories = new List<SmallCategoryModel>(selectedSmalls.OfType<SmallCategoryModel>()) });
            else if (selectedMediums.Count != 0 && selectedSmalls.Count == 0) _selectedItems.Add(new LargeCategoryModel() { Name = selectedLarge.Name, MediumCategories = new List<MediumCategoryModel>(selectedMediums.OfType<MediumCategoryModel>()) });
            else if (selectedMediums.Count != 0 && selectedSmalls.Count != 0)
            {
                var smallBelongsMedium = selectedMediums.OfType<MediumCategoryModel>().FirstOrDefault(x => x.SmallCategories.Any(y => y.Name == ((SmallCategoryModel)selectedSmalls[0]).Name));
                if (smallBelongsMedium is null)
                {
                    _selectedItems.Add(new LargeCategoryModel() { Name = selectedLarge.Name, MediumCategories = new List<MediumCategoryModel>(selectedMediums.OfType<MediumCategoryModel>()), SmallCategories = new List<SmallCategoryModel>(selectedSmalls.OfType<SmallCategoryModel>()) });
                }
                else
                {
                    var mediums = selectedMediums.OfType<MediumCategoryModel>().Where(x => x.Name != smallBelongsMedium.Name).ToList();
                    var selectedMedium = new MediumCategoryModel() { Name = smallBelongsMedium.Name, SmallCategories = new List<SmallCategoryModel>(selectedSmalls.OfType<SmallCategoryModel>()) };
                    mediums.Add(selectedMedium);
                    var large2 = new LargeCategoryModel() { Name = selectedLarge.Name, MediumCategories = mediums };
                    _selectedItems.Add(large2);
                }
            }
        }

        private void BrowsePath(object sender, RoutedEventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    _path = fbd.SelectedPath;
                }
            }
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_path))
            {
                MessageBox.Show("please, select a path");
                return;
            }

            if (_selectedItems.Any())
            {
                Task.Run(() => Parser.Start(_selectedItems, _path));
                MessageBox.Show($"Scrapping started.{Environment.NewLine}{GetLinksCount(_selectedItems)} category(ies) selected");
            }
            else MessageBox.Show("No category selected");
        }

        private void GetByUrl(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(UrlBox.Text))
            {
                MessageBox.Show("Enter a url");
                return;
            }

            if (string.IsNullOrEmpty(_path))
            {
                MessageBox.Show("please, select a path");
                return;
            }

            if (Uri.TryCreate(UrlBox.Text, UriKind.Absolute, out var uriResult) &&
                (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
            {
                var link = UrlBox.Text;
                if (link.EndsWith("/")) link = link.Insert(link.Length, Parser.Section2Page1);
                else if (link.EndsWith("?")) link = link.Insert(link.Length, Parser.Section2Page1WithoutQuestion);
                else link = link.Insert(link.Length, Parser.Section2Page1WithAmpersand);

                var pageCount = Parser.GetPageNumber(link);
                var isValid = !string.IsNullOrEmpty(FileNameBox.Text) && FileNameBox.Text.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) < 0 && !File.Exists(System.IO.Path.Combine(_path, FileNameBox.Text));
                if (isValid == false)
                {
                    MessageBox.Show("Enter a valid file name");
                    return;
                }

                var filePath = string.IsNullOrEmpty(FileNameBox.Text) ? $"{_path}\\{GetTimestampNow()}.csv" : $"{_path}\\{FileNameBox.Text}.csv";
                // sleep time is 1000ms by default
                Task.Run(() => Parser.GetItems(1000, filePath, pageCount, link));
                MessageBox.Show("Scrapping started");
            }
            else
            {
                MessageBox.Show("Url is not valid");
                return;
            }
        }

        private int GetTimestampNow()
        {
            return (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
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

        private void ResetSelection(object sender, RoutedEventArgs e)
        {
            _selectedItems.Clear();
            StartBtn.IsEnabled = false;
            MessageBox.Show("Selection cleared");
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

        private int GetLinksCount(List<LargeCategoryModel> list)
        {
            var hashLinks = new HashSet<string>();
            foreach (var large in list)
            {
                foreach (var medium in large.MediumCategories)
                {
                    foreach (var small in medium.SmallCategories)
                    {
                        hashLinks.Add(small.Link);
                    }
                }

                foreach (var small in large.SmallCategories)
                {
                    hashLinks.Add(small.Link);
                }
            }

            return hashLinks.Count;
        }
    }
}
