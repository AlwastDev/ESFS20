using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using ESFS20.MVVM.Model;
using WpfMethodsDataBaseLibrary;

namespace ESFS20.MVVM.Forms
{
    public partial class UpdateTheoryContain
    {
        private readonly int _index;
        private readonly CrudOperation _crudOperation = new CrudOperation();

        public UpdateTheoryContain(int index)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            BorderWindowUpdateTheoryContain.MouseLeftButtonDown +=
                layoutRoot_MouseLeftButtonDown;
            this._index = index;
            LoadForm();
        }

        private async void LoadForm()
        {
            RtbTextTheory.Document.Blocks.Clear();
            TbTitleTheory.Text = (await _crudOperation.SelectOperationTheoryTitleContainAsync(_index))!;
            RtbTextTheory.AppendText(await _crudOperation.SelectOperationTheoryTextContainAsync(_index));
        }

        private async void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            MyDialogBox my = new MyDialogBox("Вы действительно хотите обновить тему?");
            my.ShowDialog();
            if (!StateDialog.State) return;
            await _crudOperation.UpdateOperationTheoryAsync(_index, TbTitleTheory.Text.Replace(" ", string.Empty),
                new TextRange(RtbTextTheory.Document.ContentStart, RtbTextTheory.Document.ContentEnd).Text);
            MyMessageBox my2 = new MyMessageBox("Вы успешно обновили элемент!");
            my2.ShowDialog();
            this.Close();
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