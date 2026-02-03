using System;
using System.Windows.Forms;
using Yaadein.Data;

namespace Yaadein
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Initialize database - errors will be shown automatically
            DatabaseHelper.InitializeDatabase();

            Application.Run(new LoginForm());
        }
    }
}