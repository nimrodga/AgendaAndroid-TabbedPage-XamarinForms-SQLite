using System;
using SQLite;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using System.Windows.Input;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AgendaAndroid
{
	public partial class Home : ContentPage
	{
		HomeViewModel homeViewModel = new HomeViewModel();
		string Strfrase;
		public Home()
		{
			InitializeComponent();
			layMain.Opacity = 0.5;
			layMain.FadeTo(1, 5000);
			/*frase.Text="La imaginación es más importante que el "+
			"conocimiento, pues el conocimiento es limitado, mientras "+
			"la imaginación abarca el mundo entero. \n\n \t\t\t- Albert Einstein";*/
			frase.TextColor=Color.Black;
			
			
		}
		
		private async void OnTimePickerPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			await Task.Delay(10);
			if(e.PropertyName == TimePicker.TimeProperty.PropertyName)
			{
				var newTime = timePicker.Time;
				nameEntry.Text = nameEntry.Text + " " + newTime;
			}
		}
		
		protected override async void OnAppearing()
		{
			base.OnAppearing();
			homeViewModel.CargarFraseNueva();
			collectionView.ItemsSource = await App.Database.GetPeopleAsync();
		}
		async void SelectingDate(object sender, DateChangedEventArgs e)
		{
			// Asignamos la fecha formateada al Entry
			nameEntry.Text = nameEntry.Text + " " + e.NewDate.ToString("dd/MM/yyyy");
		}
		async void AgregarTarea_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(nameEntry.Text))
			{
				await App.Database.SavePersonAsync(new Person
				{
					Name = nameEntry.Text,
					Subscribed = subscribed.IsChecked
				});
				nameEntry.Text = string.Empty;
				subscribed.IsChecked = false;
				collectionView.ItemsSource = await App.Database.GetPeopleAsync();
			}
		}
		async void OnItemTapped(object sender, EventArgs e)
		{
			var lay = (BindableObject)sender;
			var item = (Person)lay.BindingContext;
			bool result = await DisplayAlert("Confirmar", $"¿ Seguro quiere borrar {item.Name} ?", "Si, borrar", "Cancelar");
			if(result)
			{
				await App.Database.DeletePersonAsync(item);
				(Application.Current).MainPage = new MainPage();
			}
		}
		async void OnStateTapped(object sender, EventArgs e)
		{
			var lay = (BindableObject)sender;
			var item = (Person)lay.BindingContext; 
			bool result = await DisplayAlert("Confirmar", $"¿ Seguro quiere cambiar el estado de {item.Name} ?", "Si, cambiar", "Cancelar");
			if(result)
			{
				await App.Database.ToggleItemStatusAsync(item);
				(Application.Current).MainPage = new MainPage();
			}
		}
		private async void OnRandomFraseTapped(object sender, EventArgs e)
		{
			frase.Text = homeViewModel.TextoFrase;
			string voice = frase.Text;
			TextToSpeech.SpeakAsync($"{voice}");
		}
	}
	public class HomeViewModel : BaseViewModel
	{
		public ICommand CambiarFraseCommand { get; }
		private string _textoFrase;
		public HomeViewModel()
		{
			CambiarFraseCommand = new Command(async () => await CargarFraseNueva());
		}
		public string TextoFrase
		{
			get => _textoFrase;
			set => SetProperty(ref _textoFrase, value);
		}
		public async Task CargarFraseNueva()
		{
			var strfrase = await App.FraseDataBase.GetRandomFraseAsync();
			TextoFrase = strfrase?.Texto?? "No hay frases disponibles";
		}
	}
	
}
