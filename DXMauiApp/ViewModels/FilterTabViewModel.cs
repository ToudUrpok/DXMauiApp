using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DXMauiApp.Data;

namespace DXMauiApp.ViewModels;

public class FilterTabViewModel<T> : INotifyPropertyChanged
{
    private bool isSelected = false;
    public string Title { get; }
    public IReadOnlyList<T> FilteredList { get; }

    // This property is used to change the appearance of a tab depending on its state. 
    public bool IsSelected
    {
        get { return isSelected; }
        set
        {
            if (value == isSelected) return;
            isSelected = value;
            RaisePropertyChanged();
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public FilterTabViewModel(string title, IEnumerable<T> filteredList)
    {
        Title = title;
        FilteredList = filteredList.ToList();
    }
    private void RaisePropertyChanged([CallerMemberName] string caller = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
    }
}
