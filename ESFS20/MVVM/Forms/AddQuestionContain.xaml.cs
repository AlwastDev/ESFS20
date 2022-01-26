using ESFS20.MVVM.Model;
using System.Windows;
using System.Windows.Input;
using WpfMethodsDataBaseLibrary;

namespace ESFS20.MVVM.Forms
{
    public partial class AddQuestionContain
    {
        private readonly CrudOperation _crudOperation = new CrudOperation();

        public AddQuestionContain()
        {
            InitializeComponent();
            BorderWindowAddQuestionContain.MouseLeftButtonDown +=
                layoutRoot_MouseLeftButtonDown;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void layoutRoot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseForm_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void BtnAddQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (!await _crudOperation.IsCheckQuestionAsync(TbTitleQuestion.Text, IdCurrentTest.IdTest))
            {
                if (!(CbAnswerOne.IsChecked == true && CbAnswerTwo.IsChecked == true && CbAnswerThree.IsChecked == true
                      || CbAnswerOne.IsChecked == false && CbAnswerTwo.IsChecked == true &&
                      CbAnswerThree.IsChecked == true
                      || CbAnswerOne.IsChecked == true && CbAnswerTwo.IsChecked == false &&
                      CbAnswerThree.IsChecked == true
                      || CbAnswerOne.IsChecked == true && CbAnswerTwo.IsChecked == true &&
                      CbAnswerThree.IsChecked == false))
                {
                    if (TbTitleAnswer1.Text == TbTitleAnswer2.Text || TbTitleAnswer1 == TbTitleAnswer3 ||
                        TbTitleAnswer3.Text == TbTitleAnswer1.Text)
                    {
                        MyMessageBox my = new MyMessageBox("У вас указаны одинаковые ответы!");
                        my.ShowDialog();
                    }
                    else if (TbTitleAnswer1.Text == string.Empty ||
                             TbTitleAnswer2.Text == string.Empty || TbTitleAnswer3.Text == string.Empty)
                    {
                        MyMessageBox my = new MyMessageBox("Укажите ответы!");
                        my.ShowDialog();
                    }
                    else if (CbAnswerOne.IsChecked == false && CbAnswerTwo.IsChecked == false && CbAnswerThree.IsChecked == false)
                    {
                        MyMessageBox my = new MyMessageBox("Укажите правильный ответ!");
                        my.ShowDialog();
                    }
                    else
                    {
                        await _crudOperation.InsertOperationQuestionAsync(TbTitleQuestion.Text, IdCurrentTest.IdTest);
                        bool one = CbAnswerOne.IsChecked == true;
                        bool two = CbAnswerTwo.IsChecked == true;
                        bool three = CbAnswerThree.IsChecked == true;

                        await _crudOperation.InsertOperationAnswerAsync(TbTitleAnswer1.Text,
                            await _crudOperation.GetQuestionIdAsync(TbTitleQuestion.Text, IdCurrentTest.IdTest), one);
                        await _crudOperation.InsertOperationAnswerAsync(TbTitleAnswer2.Text,
                            await _crudOperation.GetQuestionIdAsync(TbTitleQuestion.Text, IdCurrentTest.IdTest), two);
                        await _crudOperation.InsertOperationAnswerAsync(TbTitleAnswer3.Text,
                            await _crudOperation.GetQuestionIdAsync(TbTitleQuestion.Text, IdCurrentTest.IdTest), three);
                        MyMessageBox my = new MyMessageBox("Вы успешно добавили вопрос!");
                        my.ShowDialog();
                        this.Close();
                    }
                }
                else
                {
                    MyMessageBox my = new MyMessageBox("У вас выбрано несколько правильных ответов!");
                    my.ShowDialog();
                }
            }
            else
            {
                MyMessageBox my = new MyMessageBox("Название вопроса уже существует в тесте!");
                my.ShowDialog();
            }
        }
    }
}