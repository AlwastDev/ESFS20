using ESFS20.MVVM.Forms;
using ESFS20.MVVM.Model;
using System;
using System.Windows;
using WpfMethodsDataBaseLibrary;

namespace ESFS20.MVVM.View
{
    public partial class AuthorizationView
    {
        private readonly CrudOperation _crudOperation = new CrudOperation();

        public AuthorizationView()
        {
            InitializeComponent();
        }

        private async void BtnAuthorization_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (await _crudOperation.IsAuthAccountAsync(TbLogin.Text, PbPassword.Password))
                {
                    SplashScreen splash = new SplashScreen();
                    Application.Current.Windows[0]?.Close();
                    splash.IsAuth = true;
                    IdCurrentAccount.IdAccount = await _crudOperation.GetIdAccountAsync(TbLogin.Text);
                    splash.IsAdmin = await _crudOperation.IsAdminAsync(TbLogin.Text);
                    splash.ShowDialog();
                }
                else
                {
                    TbLogin.Text = "";
                    PbPassword.Password = "";
                    MyMessageBox my = new MyMessageBox("Неверный логин или пароль!");
                    my.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox my = new MyMessageBox(ex.Message);
                my.ShowDialog();
            }
        }
    }
}