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

namespace Farplane.FFX2
{
    /// <summary>
    /// Interaction logic for CommandSelectDialog.xaml
    /// </summary>
    public partial class CommandSelectDialog : MetroWindow
    {
        private List<string> _searchList = new List<string>();
        private List<string> searchResults = new List<string>();
        public int SearchResult = -1;
        public string lastSearch = string.Empty;

        public CommandSelectDialog(List<string> searchList)
        {
            InitializeComponent();

            _searchList = searchList;

            ListCommandSearch.ItemsSource = searchResults;
            TextCommandSearch.Focus();
        }

        public void SearchForCommand(string searchString)
        {
            var lCaseSearch = searchString.ToLower();

            var result = _searchList.FindAll(item => item.ToLower().Contains(lCaseSearch));
            if (result.Count == 0) return;

            searchResults.Clear();
            foreach (var s in result)
                searchResults.Add(s);

            ListCommandSearch.Items.Refresh();
            if(searchResults.Count > 0)
                ListCommandSearch.SelectedIndex = 0;

            TextCommandSearch.Text = searchString;
            TextCommandSearch.SelectionStart = 0;
            TextCommandSearch.SelectionLength = searchString.Length;
            TextCommandSearch.Focus();
        }

        private void TextCommandSearch_OnKeyDown(object sender, KeyEventArgs e)
        {


            switch (e.Key)
            {
                case Key.Enter:
                    // Confirm choice if pressed twice
                    if (lastSearch == TextCommandSearch.Text)
                        CloseAndReturn();
                    // Execute search
                    SearchForCommand(TextCommandSearch.Text);
                    lastSearch = TextCommandSearch.Text;
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
            CloseAndReturn();
        }

        private void CloseAndReturn()
        {
            if (ListCommandSearch.SelectedItems.Count != 1) return;
            var searchIndex = _searchList.IndexOf(ListCommandSearch.SelectedItem.ToString());
            SearchResult = searchIndex;
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
            CloseAndReturn();

        }

        private void ButtonUseNoAbility_OnClick(object sender, RoutedEventArgs e)
        {
            SearchResult = -1;
            DialogResult = true;
            Close();
        }
    }
}
