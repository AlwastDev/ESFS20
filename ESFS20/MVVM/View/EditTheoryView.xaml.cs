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
    public partial class EditTheoryView
    {
        private const int Wait = 30; //Время ожидания перед обновлением
        private TimeSpan _time;
        private readonly CrudOperation _crudOperation = new CrudOperation();

        public EditTheoryView()
        {
            InitializeComponent();
            LoadForm();
            _time = TimeSpan.FromSeconds(Wait);
            var dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Tick += Timer_Tick;
            dt.Start();
        }

        private async void LoadForm()
        {
            await LoadDataGrid();
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            if (_time == TimeSpan.Zero)
            {
                await LoadDataGrid();
                _time = TimeSpan.FromSeconds(Wait);
            }
            else
            {
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }
        }

        private async Task LoadDataGrid()
        {
            DgvEditTheory.ItemsSource = "";
            DgvEditTheory.ItemsSource = await _crudOperation.SelectOperationTheoryAsync();
            DgvEditTheory.Columns.RemoveAt(2);
        }

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddTheoryContain addTheoryContain = new AddTheoryContain();
            addTheoryContain.ShowDialog();
            await LoadDataGrid();
        }

        private async void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (DgvEditTheory.SelectedItem != null)
            {
                var id =
                    (DgvEditTheory.Columns[0]
                        .GetCellContent(DgvEditTheory.Items[DgvEditTheory.SelectedIndex]) as TextBlock)?.Text;
                if (id != null)
                {
                    UpdateTheoryContain updateTheoryContain = new UpdateTheoryContain(int.Parse(id));
                    updateTheoryContain.ShowDialog();
                }
            }
            else
            {
                MyMessageBox my1 = new MyMessageBox("Выберите тему!");
                my1.ShowDialog();
            }
            await LoadDataGrid();
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DgvEditTheory.SelectedItem != null)
            {
                MyDialogBox my = new MyDialogBox("Вы действительно хотите удалить тему?");
                my.ShowDialog();
                if (!StateDialog.State) return;
                try
                {
                    var id = (DgvEditTheory.Columns[0]
                        .GetCellContent(DgvEditTheory.Items[DgvEditTheory.SelectedIndex]) as TextBlock)?.Text;
                    if (id != null) await _crudOperation.DeleteOperationTheoryAsync(int.Parse(id));
                    MyMessageBox my2 = new MyMessageBox("Вы успешно удалили тему!");
                    my2.ShowDialog();
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
                MyMessageBox my2 = new MyMessageBox("Выберите тему!");
                my2.ShowDialog();
            }
        }
    }
}