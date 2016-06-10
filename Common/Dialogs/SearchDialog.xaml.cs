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
using Farplane.FFX2.Values;
using MahApps.Metro.Controls;

namespace Farplane.Common.Dialogs
{
    /// <summary>
    /// Interaction logic for CommandSelectDialog.xaml
    /// </summary>
    public partial class SearchDialog : MetroWindow
    {
        private readonly List<string> _searchList = new List<string>();
        private readonly List<string> _searchResults = new List<string>();
        private string _lastSearch = string.Empty;

        public int ResultIndex = -1;

        public SearchDialog(List<string> searchList, string defaultSearch = "", bool showNoSelection = true)
        {
            InitializeComponent();

            ButtonClearSlot.Visibility = showNoSelection ? Visibility.Visible : Visibility.Collapsed;

            _searchList = searchList;

            ListCommandSearch.ItemsSource = _searchResults;

            if (defaultSearch != "")
                PerformSearch(defaultSearch);

            _lastSearch = int.MaxValue.ToString();

            TextSearchBox.Focus();
        }

        public void PerformSearch(string searchString)
        {
            var lCaseSearch = searchString.ToLower();

            var result = _searchList.FindAll(item => item.ToLower().Contains(lCaseSearch));
            if (result.Count == 0) return;

            _searchResults.Clear();
            foreach (var s in result)
                _searchResults.Add(s);

            ListCommandSearch.Items.Refresh();
            if(_searchResults.Count > 0)
                ListCommandSearch.SelectedIndex = 0;

            TextSearchBox.Text = searchString;
            TextSearchBox.SelectionStart = 0;
            TextSearchBox.SelectionLength = searchString.Length;
            TextSearchBox.Focus();
        }

        private void TextCommandSearch_OnKeyDown(object sender, KeyEventArgs e)
        {


            switch (e.Key)
            {
                case Key.Enter:
                    // Confirm choice if pressed twice
                    if (_lastSearch == TextSearchBox.Text)
                        TryCloseAndReturn();
                    // Execute search
                    PerformSearch(TextSearchBox.Text);
                    _lastSearch = TextSearchBox.Text;
                    break;
                case Key.Escape:
                    Close();
                    return;
                default:
                    return;
            }
            
        }

        private void ButtonSelectedCommand_OnClick(object sender, RoutedEventArgs e)
        {
            TryCloseAndReturn();
        }

        private void TryCloseAndReturn()
        {
            if (ListCommandSearch.SelectedItems.Count != 1) return;
            var searchIndex = _searchList.IndexOf(ListCommandSearch.SelectedItem.ToString());
            ResultIndex = searchIndex;
            DialogResult = true;
            this.Close();
        }

        private void ButtonCancelSelection_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void ListCommandSearch_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TryCloseAndReturn();
        }

        private void ButtonClearSlot_Click(object sender, RoutedEventArgs e)
        {
            ResultIndex = -1;
            DialogResult = true;
            Close();
        }
    }
}
