using System.ComponentModel;
using DXMauiApp.Data;

namespace DXMauiApp.ViewModels;

public class EmployeeDataViewModel : INotifyPropertyChanged
{
    readonly EmployeeData data;

    public event PropertyChangedEventHandler PropertyChanged;
    public IReadOnlyList<Employee> Employees { get => data.Employees; }

    public EmployeeDataViewModel()
    {
        data = new EmployeeData();
    }

    protected void RaisePropertyChanged(string name)
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(name));
    }
}
