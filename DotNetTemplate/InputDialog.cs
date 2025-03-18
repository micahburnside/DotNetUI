using System.Windows;
using System.Windows.Controls;

namespace NETTemplate.Controls
{
    public class InputDialog : Window
    {
        private readonly TextBox _textBox;
        public string ItemName => _textBox.Text;

        public InputDialog(string title, string prompt, string initialText = "")
        {
            Title = title;
            Width = 300;
            Height = 150;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            _textBox = new TextBox
            {
                Margin = new Thickness(10),
                Width = 250,
                Height = 30,
                Text = initialText
            };

            var label = new Label
            {
                Content = prompt,
                Margin = new Thickness(10, 10, 10, 5)
            };

            var okButton = new CustomButton("OK", 80, 30);
            okButton.SetClickAction((s, e) => { DialogResult = true; Close(); });

            var cancelButton = new CustomButton("Cancel", 80, 30);
            cancelButton.SetClickAction((s, e) => Close());

            var buttonPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Right
            };
            buttonPanel.Children.Add(okButton);
            buttonPanel.Children.Add(cancelButton);

            var mainPanel = new StackPanel
            {
                Margin = new Thickness(10)
            };
            mainPanel.Children.Add(label);
            mainPanel.Children.Add(_textBox);
            mainPanel.Children.Add(buttonPanel);

            Content = mainPanel;
        }
    }
}