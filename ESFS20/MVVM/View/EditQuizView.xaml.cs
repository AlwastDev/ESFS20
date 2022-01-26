using System.Windows.Controls;
using WpfMethodsDataBaseLibrary;
using System;
using System.Windows;
using System.Windows.Threading;
using System.Threading.Tasks;
using ESFS20.MVVM.Forms;
using ESFS20.MVVM.Model;

namespace ESFS20.MVVM.View
{
    public partial class EditQuizView
    {
        private const int Wait = 30; //Время ожидания перед обновлением
        private TimeSpan _time;
        private readonly CrudOperation _crudOperation = new CrudOperation();

        public EditQuizView()
        {
            InitializeComponent();
            ReloadForm();
            _time = TimeSpan.FromSeconds(Wait);
            var dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Tick += Timer_Tick;
            dt.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_time == TimeSpan.Zero)
            {
                ReloadForm();
                _time = TimeSpan.FromSeconds(Wait);
            }
            else
            {
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }
        }

        private async void ReloadForm()
        {
            await LoadDataGrid();
        }

        private async Task LoadDataGrid()
        {
            DgvEditQuiz.ItemsSource = "";
            DgvEditQuiz.ItemsSource = await _crudOperation.SelectOperationTestAsync();
            DgvEditQuiz.Columns.RemoveAt(2);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddQuizContain addQuizContain = new AddQuizContain();
            addQuizContain.ShowDialog();
            ReloadForm();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            //NO WORKING
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (DgvEditQuiz.SelectedItem != null)
            {
                MyDialogBox my = new MyDialogBox("Вы действительно хотите удалить тест?");
                my.ShowDialog();
                if (!StateDialog.State) return;
                try
                {
                    var id =
                        (DgvEditQuiz.Columns[0]
                            .GetCellContent(DgvEditQuiz.Items[DgvEditQuiz.SelectedIndex]) as TextBlock)?.Text;
                    if (id != null)
                    {
                        //await _crudOperation.DeleteOperationAnswerAsync(
                          //  await _crudOperation.GetQuestionIdNoNameAsync(int.Parse(id)));
                        //await _crudOperation.DeleteOperationQuestionAsync(int.Parse(id));
                        await _crudOperation.DeleteOperationTestAsync(int.Parse(id));
                    }
                    ReloadForm();
                }
                catch (Exception ex)
                {
                    MyMessageBox my1 = new MyMessageBox(ex.Message);
                    my1.ShowDialog();
                }
            }
            else
            {
                MyMessageBox my2 = new MyMessageBox("Выберите элемент!");
                my2.ShowDialog();
            }
        }
    }
}