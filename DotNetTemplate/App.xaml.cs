using System.Configuration;
using System.Data;
using System.Windows;

namespace NETTemplate
{
    public class App : Application
    {
        [STAThread]
        public static void Main()
        {
            var app = new App();
            app.Run(new MainWindow());
        }
    }
}