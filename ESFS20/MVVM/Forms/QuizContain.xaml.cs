using ESFS20.MVVM.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WpfMethodsDataBaseLibrary;

namespace ESFS20.MVVM.Forms
{
    public partial class QuizContain
    {
        private readonly CrudOperation _crudOperation = new CrudOperation();
        private readonly List<string> _questions = new List<string>();
        private readonly List<string> _answers = new List<string>();
        private bool _isLoaded;
        private readonly int _currentTestId;
        private int _currentMark;

        public QuizContain(int index)
        {
            _currentTestId = index;
            InitializeComponent();
            BorderWindowQuizContain.MouseLeftButtonDown += layoutRoot_MouseLeftButtonDown;
            LoadForm();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private async void LoadForm()
        {
            CbAnswerOne.IsChecked = false;
            CbAnswerTwo.IsChecked = false;
            CbAnswerThree.IsChecked = false;
            if (_isLoaded != true)
            {
                _questions.AddRange(await _crudOperation.GetQuestionsAsync(
                    await _crudOperation.GetTestIdAsync(
                        (await _crudOperation.SelectOperationQuizTitleContainAsync(_currentTestId))!))); //Коллекция вопросов
                _isLoaded = true;
            }

            _answers.AddRange(
                await _crudOperation.GetAnswersAsync(
                    await _crudOperation.GetQuestionIdAsync(_questions.ElementAt(0), _currentTestId))); //Коллекция ответов
            TitleQuiz.Text = await _crudOperation.SelectOperationQuizTitleContainAsync(_currentTestId); //Название теста
            TextQuestion.Text = _questions.ElementAt(0); //Название текущего вопроса

            TextAnswerOne.Text = _answers.ElementAt(0);
            TextAnswerTwo.Text = _answers.ElementAt(1);
            TextAnswerThree.Text = _answers.ElementAt(2);
        }

        private async void BtnSendAnswer_Click(object sender, RoutedEventArgs e)
        {
            if (_questions.Count() != 1)
            {
                _answers.Clear();
                if (CbAnswerOne.IsChecked == true && CbAnswerTwo.IsChecked == false &&
                    CbAnswerThree.IsChecked == false) //Проверка правильности ответа
                {
                    if (await _crudOperation.GetTrueAnswerAsync(
                            await _crudOperation.GetQuestionIdAsync(_questions.ElementAt(0), _currentTestId)) ==
                        TextAnswerOne.Text)
                    {
                        await _crudOperation.UpdateOperationAccountAsync(IdCurrentAccount.IdAccount, 10);
                        _currentMark += 10;
                    }
                }
                else if (CbAnswerTwo.IsChecked == true && CbAnswerOne.IsChecked == false &&
                         CbAnswerThree.IsChecked == false)
                {
                    if (await _crudOperation.GetTrueAnswerAsync(
                            await _crudOperation.GetQuestionIdAsync(_questions.ElementAt(0), _currentTestId)) ==
                        TextAnswerTwo.Text)
                    {
                        await _crudOperation.UpdateOperationAccountAsync(IdCurrentAccount.IdAccount, 10);
                        _currentMark += 10;
                    }
                }
                else if (CbAnswerThree.IsChecked == true && CbAnswerTwo.IsChecked == false &&
                         CbAnswerOne.IsChecked == false)
                {
                    if (await _crudOperation.GetTrueAnswerAsync(
                            await _crudOperation.GetQuestionIdAsync(_questions.ElementAt(0), _currentTestId)) ==
                        TextAnswerThree.Text)
                    {
                        await _crudOperation.UpdateOperationAccountAsync(IdCurrentAccount.IdAccount, 10);
                        _currentMark += 10;
                    }
                }

                _questions.RemoveAt(0);
                LoadForm();
            }
            else
            {
                if (CbAnswerOne.IsChecked == true && CbAnswerTwo.IsChecked == false &&
                    CbAnswerThree.IsChecked == false) //Проверка правильности ответа
                {
                    if (await _crudOperation.GetTrueAnswerAsync(
                            await _crudOperation.GetQuestionIdAsync(_questions.ElementAt(0), _currentTestId)) ==
                        TextAnswerOne.Text)
                    {
                        await _crudOperation.UpdateOperationAccountAsync(IdCurrentAccount.IdAccount, 10);
                        _currentMark += 10;
                    }
                }
                else if (CbAnswerTwo.IsChecked == true && CbAnswerOne.IsChecked == false &&
                         CbAnswerThree.IsChecked == false)
                {
                    if (await _crudOperation.GetTrueAnswerAsync(
                            await _crudOperation.GetQuestionIdAsync(_questions.ElementAt(0), _currentTestId)) ==
                        TextAnswerTwo.Text)
                    {
                        await _crudOperation.UpdateOperationAccountAsync(IdCurrentAccount.IdAccount, 10);
                        _currentMark += 10;
                    }
                }
                else if (CbAnswerThree.IsChecked == true && CbAnswerTwo.IsChecked == false &&
                         CbAnswerOne.IsChecked == false)
                {
                    if (await _crudOperation.GetTrueAnswerAsync(
                            await _crudOperation.GetQuestionIdAsync(_questions.ElementAt(0), _currentTestId)) ==
                        TextAnswerThree.Text)
                    {
                        await _crudOperation.UpdateOperationAccountAsync(IdCurrentAccount.IdAccount, 10);
                        _currentMark += 10;
                    }
                }

                MyMessageBox my = new MyMessageBox("Вы прошли тест! Ваша оценка: " + _currentMark.ToString());
                my.ShowDialog();
                this.Close();
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