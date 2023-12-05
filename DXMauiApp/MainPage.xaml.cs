using DevExpress.Maui.DataGrid;
using DXMauiApp.Data;

namespace DXMauiApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void DataGridView_CustomUnboundData(object sender, CustomUnboundDataEventArgs e)
        {
            var grid = (DataGridView)sender;
            if (e.Column.FieldName == "Age" && e.IsGetData)
            {
                var employee = (Employee)e.Item;
                e.Value = (int)((DateTime.Now - employee.BirthDate).TotalDays / 365);
            }
        }

        private void ShowColumnChooserClick(object sender, EventArgs e)
        {
            columnChooserPopup.IsOpen = true;
        }
    }
}