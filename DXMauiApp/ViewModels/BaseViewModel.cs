﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using DXMauiApp.Data;
using DXMauiApp.Services;

namespace DXMauiApp.ViewModels;

public class BaseViewModel : INotifyPropertyChanged
{
	public IDataStore<Post> DataStore => DependencyService.Get<IDataStore<Post>>();

    bool _isBusy;
    public bool IsBusy
	{
		get => _isBusy;
		set => SetProperty(ref _isBusy, value);
	}

    public event PropertyChangedEventHandler PropertyChanged;

	protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null)
	{
		if (EqualityComparer<T>.Default.Equals(backingStore, value))
			return false;

		backingStore = value;
		onChanged?.Invoke();
		OnPropertyChanged(propertyName);
		return true;
	}

	protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

}