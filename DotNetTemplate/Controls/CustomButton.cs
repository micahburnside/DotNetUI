using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using NETTemplate.Styles;

namespace NETTemplate.Controls
{
    public class CustomButton : Button
    {
        public CustomButton(string text, double width = 100, double height = 30)
        {
            Content = text;
            Width = width;
            Height = height;
            Background = ControlStyles.DefaultButtonBackground;
            Foreground = Brushes.White;
            FontSize = 14;
            BorderThickness = new Thickness(0);
            Margin = new Thickness(5);
            HorizontalAlignment = HorizontalAlignment.Stretch;

            // Hover effect
            MouseEnter += (s, e) => Background = ControlStyles.HoverButtonBackground;
            MouseLeave += (s, e) => Background = ControlStyles.DefaultButtonBackground;
        }

        public void SetClickAction(RoutedEventHandler handler)
        {
            Click += handler;
        }
    }
}