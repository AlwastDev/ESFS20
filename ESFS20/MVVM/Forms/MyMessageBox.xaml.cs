using System.Windows;
namespace ESFS20.MVVM.Forms
{
    public partial class MyMessageBox
    {
        public MyMessageBox(string text)
        {
            InitializeComponent();
            TbText.Text = text;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
