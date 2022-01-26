using System.Windows;
using System.Windows.Input;

namespace ESFS20
{
    public partial class MainWindow
    {
        public MainWindow(bool isAuth, bool isAdmin)
        {
            InitializeComponent();
            BorderMainWindow.MouseLeftButtonDown += layoutRoot_MouseLeftButtonDown;
            switch (isAuth)
            {
                case true when isAdmin == false:
                    HomeAuth.IsChecked = true;
                    RadioBtnsAuth.Visibility = Visibility.Visible;
                    RadioBtnsNoAuth.Visibility = Visibility.Collapsed;
                    RadioBtnsAdmin.Visibility = Visibility.Collapsed;
                    TbNameAccount.Text = "Студент";
                    break;
                case true when true:
                    HomeAdmin.IsChecked = true;
                    RadioBtnsAdmin.Visibility = Visibility.Visible;
                    RadioBtnsNoAuth.Visibility = Visibility.Collapsed;
                    RadioBtnsAuth.Visibility = Visibility.Collapsed;
                    TbNameAccount.Text = "Преподаватель";
                    break;
                default:
                    HomeNoAuth.IsChecked = true;
                    RadioBtnsNoAuth.Visibility = Visibility.Visible;
                    RadioBtnsAdmin.Visibility = Visibility.Collapsed;
                    RadioBtnsAuth.Visibility = Visibility.Collapsed;
                    TbNameAccount.Text = "";
                    break;
            }
        }

        private void layoutRoot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
    }
}