using System.Windows.Controls;
using WpfMethodsDataBaseLibrary;
using System;
using System.Windows.Threading;
using System.Threading.Tasks;
using ESFS20.MVVM.Forms;

namespace ESFS20.MVVM.View
{
    public partial class QuizView
    {
        private const int Wait = 30; //Время ожидания перед обновлением
        private TimeSpan _time;
        private readonly CrudOperation _crudOperation = new CrudOperation();

        public QuizView()
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
            DgvQuiz.ItemsSource = "";
            DgvQuiz.ItemsSource = await _crudOperation.SelectOperationTestAsync();
            DgvQuiz.Columns.RemoveAt(2);
        }

        private void Row_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var id = (DgvQuiz.Columns[0].GetCellContent(DgvQuiz.Items[DgvQuiz.SelectedIndex]) as TextBlock)?.Text;
            if (id == null) return;
            QuizContain quiz = new QuizContain(int.Parse(id));
            quiz.ShowDialog();
        }
    }
}