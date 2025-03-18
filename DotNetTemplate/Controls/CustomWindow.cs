using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NETTemplate.Controls
{
    public class CustomWindow : Window
    {
        private readonly Grid _mainGrid;

        public CustomWindow(string title, double width = 800, double height = 600)
        {
            Title = title;
            Width = width;
            Height = height;
            WindowStyle = WindowStyle.SingleBorderWindow;
            Background = Brushes.WhiteSmoke;

            _mainGrid = new Grid();
            Content = _mainGrid;
        }

        public void AddControl(FrameworkElement control, int row = 0, int column = 0)
        {
            if (_mainGrid.RowDefinitions.Count <= row)
                _mainGrid.RowDefinitions.Add(new RowDefinition());
            if (_mainGrid.ColumnDefinitions.Count <= column)
                _mainGrid.ColumnDefinitions.Add(new ColumnDefinition());

            Grid.SetRow(control, row);
            Grid.SetColumn(control, column);
            _mainGrid.Children.Add(control);
        }
    }
}