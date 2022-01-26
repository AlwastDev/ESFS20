using ESFS20.MVVM.Forms;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;
using WpfMethodsDataBaseLibrary;

namespace ESFS20.MVVM.View
{
    public partial class TheoryView
    {
        private const int Wait = 30; //Время ожидания перед обновлением
        private TimeSpan _time;
        private readonly CrudOperation _crudOperation = new CrudOperation();
        public TheoryView()
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
            if (_time == TimeSpan.Zero) {
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
            await LoadDataGridAsync();
        }

        private async Task LoadDataGridAsync()
        {
            DgvTheory.ItemsSource = "";
            DgvTheory.ItemsSource = await _crudOperation.SelectOperationTheoryAsync();
            DgvTheory.Columns.RemoveAt(2);
        }

        private void Row_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var id = (DgvTheory.Columns[0].GetCellContent(DgvTheory.Items[DgvTheory.SelectedIndex]) as TextBlock)?.Text;
            if (id == null) return;
            TheoryContain theory = new TheoryContain(int.Parse(id));
            theory.ShowDialog();
        }
    }
}
