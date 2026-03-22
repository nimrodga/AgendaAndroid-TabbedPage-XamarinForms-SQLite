using System;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

namespace AgendaAndroid
{
    public class BaseViewModel : INotifyPropertyChanged
    {         
        public event PropertyChangedEventHandler PropertyChanged;

    	// Metodo virtual para lanzar el evento PropertyChanged
    	protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    	{
        	PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    	}

    	//Metodo auxiliar para establecer el valor de propiedad y llamar el evento cuando el valor ha cambiado
    	protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    	{
        	if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        	field = value;
        	OnPropertyChanged(propertyName);
        	return true;
    	}
    }
}
