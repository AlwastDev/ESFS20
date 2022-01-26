using ESFS20.MVVM.Model;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using WpfMethodsDataBaseLibrary;

namespace ESFS20.MVVM.Forms
{
    public partial class UpdateQuestionContain
    {
        private readonly int _index;
        private readonly CrudOperation _crudOperation = new CrudOperation();
        private List<string> _titleAnswers = new List<string>();
        private List<bool?> _rightAnswers = new List<bool?>();

        public UpdateQuestionContain(int index)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            BorderWindowUpdateQuestionContain.MouseLeftButtonDown +=
                layoutRoot_MouseLeftButtonDown;
            this._index = index;
            LoadForm(this._index);
        }

        private async void LoadForm(int index)
        {
            _titleAnswers = await _crudOperation.SelectOperationAnswerTitleContainAsync(index);
            _rightAnswers = await _crudOperation.SelectOperationAnswerRightContainAsync(index);
            TbTitleQuestion.Text = (await _crudOperation.SelectOperationQuestionTitleContainAsync(index))!;
            TbTitleAnswer1.Text = _titleAnswers[0];
            TbTitleAnswer2.Text = _titleAnswers[1];
            TbTitleAnswer3.Text = _titleAnswers[2];
            CbAnswerOne.IsChecked = _rightAnswers[0];
            CbAnswerTwo.IsChecked = _rightAnswers[1];
            CbAnswerThree.IsChecked = _rightAnswers[2];
        }

        private async void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            MyDialogBox my = new MyDialogBox("Вы действительно хотите обновить вопрос?");
            my.ShowDialog();
            if (!StateDialog.State) return;
            bool one = CbAnswerOne.IsChecked == true;
            bool two = CbAnswerTwo.IsChecked == true;
            bool three = CbAnswerThree.IsChecked == true;

            await _crudOperation.UpdateOperationQuestionAsync(_index, TbTitleQuestion.Text, IdCurrentTest.IdTest);
            await _crudOperation.UpdateOperationAnswerAsync(
                await _crudOperation.GetAnswerIdAsync(_titleAnswers[0], _index),
                TbTitleAnswer1.Text, one, _index);
            await _crudOperation.UpdateOperationAnswerAsync(
                await _crudOperation.GetAnswerIdAsync(_titleAnswers[1], _index),
                TbTitleAnswer2.Text, two, _index);
            await _crudOperation.UpdateOperationAnswerAsync(
                await _crudOperation.GetAnswerIdAsync(_titleAnswers[2], _index),
                TbTitleAnswer3.Text, three, _index);
            MyMessageBox my2 = new MyMessageBox("Вы успешно обновили вопрос!");
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