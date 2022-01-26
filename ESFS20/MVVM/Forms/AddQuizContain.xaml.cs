using ESFS20.MVVM.Model;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfMethodsDataBaseLibrary;

namespace ESFS20.MVVM.Forms
{
    public partial class AddQuizContain
    {
        private readonly CrudOperation _crudOperation = new CrudOperation();

        public AddQuizContain()
        {
            InitializeComponent();
            BtnAddQuestion.Visibility = Visibility.Hidden;
            BtnUpdateQuestion.Visibility = Visibility.Hidden;
            BtnDeleteQuestion.Visibility = Visibility.Hidden;
            BorderWindowAddQuizContain.MouseLeftButtonDown +=
                layoutRoot_MouseLeftButtonDown;
            TbTitleTestQuiz.IsEnabled = true;
            BtnAddTest.Visibility = Visibility.Visible;
            BtnCreateTest.Visibility = Visibility.Visible;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private async Task LoadDataGrid()
        {
            DgvGetTest.ItemsSource = "";
            DgvGetTest.ItemsSource = await _crudOperation.SelectOperationQuestionTestAsync(IdCurrentTest.IdTest);
            DgvGetTest.Columns.RemoveAt(4);
            DgvGetTest.Columns.RemoveAt(3);
            DgvGetTest.Columns.RemoveAt(2);
        }

        private async void BtnAddQuestion_Click(object sender, RoutedEventArgs e)
        {
            AddQuestionContain addQuestionContain = new AddQuestionContain();
            addQuestionContain.ShowDialog();
            await LoadDataGrid();
        }

        private void BtnUpdateQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (DgvGetTest.SelectedItem != null)
            {
                var id =
                    (DgvGetTest.Columns[0].GetCellContent(DgvGetTest.Items[DgvGetTest.SelectedIndex]) as TextBlock)
                    ?.Text;
                if (id != null)
                {
                    UpdateQuestionContain updateQuestionContain = new UpdateQuestionContain(int.Parse(id));
                    updateQuestionContain.ShowDialog();
                }

                Task.Run(LoadDataGrid);
            }
            else
            {
                MyMessageBox my1 = new MyMessageBox("Выберите вопрос!");
                my1.ShowDialog();
            }
        }

        private async void BtnDeleteQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (DgvGetTest.SelectedItem != null)
            {
                MyDialogBox my = new MyDialogBox("Вы действительно хотите удалить вопрос?");
                my.ShowDialog();
                if (!StateDialog.State) return;
                try
                {
                    var id =
                        (DgvGetTest.Columns[0]
                            .GetCellContent(DgvGetTest.Items[DgvGetTest.SelectedIndex]) as TextBlock)?.Text;
                    if (id != null)
                    {
                        //await _crudOperation.DeleteOperationAnswerAsync(int.Parse(id));
                        await _crudOperation.DeleteOperationQuestionAsync(IdCurrentTest.IdTest);
                    }

                    MyMessageBox my1 = new MyMessageBox("Вы успешно удалили вопрос!");
                    my1.ShowDialog();
                    await LoadDataGrid();
                }
                catch (Exception ex)
                {
                    MyMessageBox my1 = new MyMessageBox(ex.Message);
                    my1.ShowDialog();
                }
            }
            else
            {
                MyMessageBox my2 = new MyMessageBox("Выберите вопрос!");
                my2.ShowDialog();
            }
        }

        private void BtnAddQuiz_Click(object sender, RoutedEventArgs e)
        {
            StateDialog.State = false;
            MyDialogBox my = new MyDialogBox("Вы действительно хотите выйти? Данный тест нельзя будет обновить");
            my.ShowDialog();
            if (StateDialog.State)
            {
                this.Close();
            }
        }

        private async void AddTest_Click(object sender, RoutedEventArgs e)
        {
            if (!await _crudOperation.IsCheckTestNameAsync(TbTitleTestQuiz.Text))
            {
                await _crudOperation.InsertOperationTestAsync(TbTitleTestQuiz.Text);
                IdCurrentTest.IdTest = await _crudOperation.GetTestIdAsync(TbTitleTestQuiz.Text);
                MyMessageBox my = new MyMessageBox("Вы успешно создали тест!");
                my.ShowDialog();
                BtnAddQuestion.Visibility = Visibility.Visible;
                BtnUpdateQuestion.Visibility = Visibility.Visible;
                BtnDeleteQuestion.Visibility = Visibility.Visible;
                TbTitleTestQuiz.IsEnabled = false;
                BtnAddTest.Visibility = Visibility.Visible;
                BtnCreateTest.Visibility = Visibility.Hidden;
            }
            else
            {
                MyMessageBox my = new MyMessageBox("Название теста уже существует!");
                my.ShowDialog();
            }
        }

        private void layoutRoot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private async void CloseForm_Click(object sender, RoutedEventArgs e)
        {
            if (IdCurrentTest.IdTest > 0)
            {
                await _crudOperation.DeleteOperationTestAsync(IdCurrentTest.IdTest);
            }

            this.Close();
        }
    }
}