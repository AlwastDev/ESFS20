using System;
using System.Windows.Threading;

namespace ESFS20
{
    public partial class SplashScreen
    {
        private readonly DispatcherTimer _dT = new DispatcherTimer();
        public bool IsAuth = false;
        public bool IsAdmin = false;

        public SplashScreen()
        {
            InitializeComponent();
            _dT.Tick += dt_Tick;
            _dT.Interval = new TimeSpan(0, 0, 2);
            _dT.Start();
        }

        private void dt_Tick(object sender, EventArgs e)
        {
            MainWindow main = new MainWindow(IsAuth, IsAdmin);
            main.Show();
            _dT.Stop();
            this.Close();
        }
    }
}