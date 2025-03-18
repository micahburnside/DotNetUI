using System.Windows;
using System.Windows.Controls;

namespace NETTemplate.Controls.Utilities
{
    public static class LayoutHelper
    {
        public static Grid CreateGrid(int rows, int columns)
        {
            var grid = new Grid();
            for (int i = 0; i < rows; i++) grid.RowDefinitions.Add(new RowDefinition());
            for (int i = 0; i < columns; i++) grid.ColumnDefinitions.Add(new ColumnDefinition());
            return grid;
        }
    }
}
