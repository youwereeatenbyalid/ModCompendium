using System;
using Gtk;

namespace GTKFrontend
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Application.Init();
            MainWindow win = new MainWindow();
            win.Show();
            Application.Run();
            Environment.Exit(0);
        }
    }
}
