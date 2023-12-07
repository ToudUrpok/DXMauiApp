using DevExpress.Maui.DataGrid;
using DXMauiApp.Data;

namespace DXMauiApp.Views;

public partial class DataGridContent : ContentView
{
    List<GridColumn> HideableColumns { get; }

	public DataGridContent()
	{
		InitializeComponent();

        HideableColumns = dataGrid.Columns.Where(c => !c.IsGrouped).ToList();
        columnsCollection.ItemsSource = HideableColumns;
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