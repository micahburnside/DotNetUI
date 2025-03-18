using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NETTemplate.Controls
{
    public class CustomListView : ListView
    {
        public CustomListView(double width = 300, double height = 200)
        {
            Width = width;
            Height = height;
            Background = Brushes.White;
            BorderBrush = Brushes.Gray;
            BorderThickness = new Thickness(1);
            Margin = new Thickness(5);
            HorizontalAlignment = HorizontalAlignment.Stretch;
        }

        public void AddItem(string text)
        {
            Items.Add(new ListViewItem { Content = text, Padding = new Thickness(5) });
        }
    }
}