using System;
using System.Windows.Forms;
using MovieSeriesManagementSystem.Forms;

namespace MovieSeriesManagementSystem
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {

            Application.Run(new LoginForm());
            //Application.Run(new UserDashboard());
            //Application.Run(new AddMovie());
        }
    }
}