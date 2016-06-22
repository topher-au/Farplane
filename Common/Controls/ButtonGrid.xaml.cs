using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
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

namespace Farplane.Common.Controls
{
    /// <summary>
    /// Interaction logic for ButtonGrid.xaml
    /// </summary>
    public partial class ButtonGrid : UserControl
    {
        public delegate void ButtonClickedDelegate(int buttonIndex);
        public event ButtonClickedDelegate ButtonClicked;

        private int ButtonCount => GridBase.Children.Count;

        public Button[] Buttons
        {
            get
            {
                var buttons = new Button[ButtonCount];
                for (int i = 0; i < ButtonCount; i++)
                {
                    buttons[i] = (Button)GridBase.Children[i];
                }
                return buttons;
            }
        }

        public Button this[int buttonIndex] => GridBase.Children[buttonIndex] as Button;

        public bool ShowScrollBar
        {
            get { return ScrollViewer.VerticalScrollBarVisibility == ScrollBarVisibility.Visible ? true : false; }
            set
            {
                ScrollViewer.VerticalScrollBarVisibility = value
                    ? ScrollBarVisibility.Visible
                    : ScrollBarVisibility.Hidden;
            }
        }

        public ButtonGrid(int columns, int buttons)
        {
            InitializeComponent();

            for (int c = 0; c < columns; c++)
                GridBase.ColumnDefinitions.Add(new ColumnDefinition());

            for(int r=0; r< (buttons/columns); r++)
                GridBase.RowDefinitions.Add(new RowDefinition());

            if(buttons%columns == 1)
                GridBase.RowDefinitions.Add(new RowDefinition());

            for (int b = 0; b < buttons; b++)
            {
                var column = b%columns;
                var row = b/columns;
                var newButton = new Button()
                {
                    Name = "Button"+b,
                    Content = "BUTTON",
                    Margin = new Thickness(1)
                };
                Grid.SetRow(newButton, row);
                Grid.SetColumn(newButton, column);

                newButton.Click += GridButton_Click;
                GridBase.Children.Add(newButton);
            }
        }

        private void GridButton_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            var button = sender as Button;
            if (button == null) return;

            var buttonIndex = GridBase.Children.IndexOf(button);

            ButtonClicked?.Invoke(buttonIndex);
        }

        public void SetContent(int buttonIndex, object content)
        {
            var button = (Button) GridBase.Children[buttonIndex];
            if (button == null) return;

            button.Content = content;
            button.UpdateLayout();
        }
    }
}
