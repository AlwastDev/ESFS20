using ESFS20.MVVM.Model;
using System.Windows;
namespace ESFS20.MVVM.Forms
{
    public partial class MyDialogBox
    {
        public MyDialogBox(string text)
        {
            InitializeComponent();
            TbText.Text = text;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            StateDialog.State = true;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            StateDialog.State = false;
            this.Close();
        }
    }
}
