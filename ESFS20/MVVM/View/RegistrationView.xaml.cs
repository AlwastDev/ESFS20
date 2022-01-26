using ESFS20.MVVM.Forms;
using ESFS20.MVVM.Model;
using System;
using System.Windows;
using WpfMethodsDataBaseLibrary;

namespace ESFS20.MVVM.View
{
    public partial class RegistrationView
    {
        private readonly CrudOperation _crudOperation = new CrudOperation();

        public RegistrationView()
        {
            InitializeComponent();
        }

        private async void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PbPassword.Password == PbRepeatPassword.Password &&
                    await _crudOperation.IsLoginAccountAsync(TbLogin.Text) == false)
                {
                    SplashScreen splash = new SplashScreen();
                    Application.Current.Windows[0]?.Close();
                    splash.IsAuth = true;
                    splash.ShowDialog();
                    await _crudOperation.InsertOperationAccountAsync(TbLogin.Text, PbPassword.Password,
                        TbFirstName.Text, TbLastName.Text, TbMiddleName.Text);
                    IdCurrentAccount.IdAccount = await _crudOperation.GetIdAccountAsync(TbLogin.Text);
                }
                else if (await _crudOperation.IsLoginAccountAsync(TbLogin.Text))
                {
                    MyMessageBox my = new MyMessageBox("Данный логин уже существует!");
                    my.ShowDialog();
                    TbLogin.Text = "";
                    PbPassword.Password = "";
                    PbRepeatPassword.Password = "";
                }
                else
                {
                    MyMessageBox my = new MyMessageBox("Пароли не совпадают!");
                    my.ShowDialog();
                    PbPassword.Password = "";
                    PbRepeatPassword.Password = "";
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