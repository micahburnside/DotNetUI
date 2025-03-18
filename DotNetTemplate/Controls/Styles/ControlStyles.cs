using System.Windows.Media;

namespace NETTemplate.Styles
{
    public static class ControlStyles
    {
        public static SolidColorBrush DefaultButtonBackground => new SolidColorBrush(Color.FromRgb(33, 150, 243));
        public static SolidColorBrush HoverButtonBackground => new SolidColorBrush(Color.FromRgb(25, 118, 210));
    }
}