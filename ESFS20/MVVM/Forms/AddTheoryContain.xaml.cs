using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using WpfMethodsDataBaseLibrary;


namespace ESFS20.MVVM.Forms
{
    public partial class AddTheoryContain
    {
        private readonly CrudOperation _crudOperation = new CrudOperation();

        public AddTheoryContain()
        {
            InitializeComponent();
            BorderWindowAddTheoryContain.MouseLeftButtonDown +=
                layoutRoot_MouseLeftButtonDown;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (TbTitleTheory.Text != string.Empty && new TextRange(RtbTextTheory.Document.ContentStart, RtbTextTheory.Document.ContentEnd).Text != string.Empty)
            {
                await _crudOperation.InsertOperationTheoryAsync(TbTitleTheory.Text.Replace(" ", string.Empty),
                    new TextRange(RtbTextTheory.Document.ContentStart, RtbTextTheory.Document.ContentEnd).Text);
                MyMessageBox my = new MyMessageBox("Вы успешно добавили элемент!");
                my.ShowDialog();
                this.Close();
            }
            else
            {
                MyMessageBox my = new MyMessageBox("Заполните поля!");
                my.ShowDialog();
            }
        }

        private void layoutRoot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseForm_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}