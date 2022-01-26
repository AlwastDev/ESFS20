using System.Windows;
using System.Windows.Input;
using WpfMethodsDataBaseLibrary;

namespace ESFS20.MVVM.Forms
{
    public partial class TheoryContain
    {
        private readonly CrudOperation _crudOperation = new CrudOperation();

        public TheoryContain(int index)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            BorderWindowTheoryContain.MouseLeftButtonDown +=
                layoutRoot_MouseLeftButtonDown;
            LoadForm(index);
        }

        private async void LoadForm(int index)
        {
            TitleTheory.Text = await _crudOperation.SelectOperationTheoryTitleContainAsync(index);
            TextTheory.Text = await _crudOperation.SelectOperationTheoryTextContainAsync(index);
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