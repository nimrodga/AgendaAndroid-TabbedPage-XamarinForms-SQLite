using System;
using System.Linq;
using System.Xml.Linq;
using SQLite;
using Xamarin.Essentials;
using System.IO;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;

namespace AgendaAndroid
{
	public partial class Frases : ContentPage
	{
		int count;
		private readonly string[] imagePool = { "Resources/preciosa7.png", "Resources/eespego.png", "Resources/preciosa8.png", "Resources/preciosa9.png", "Resources/preciosa10.png", "Resources/preciosa11.png", "Resources/preciosa12.png", "Resources/preciosa13.png", "Resources/guapo.png", "Resources/diana.png" };
		private readonly Random random = new Random(); Label Proverbia, Pensador, MDFrases, CitaCel, Biblia;
		TapGestureRecognizer TapProverbia = new TapGestureRecognizer();
		TapGestureRecognizer TapMundiFra = new TapGestureRecognizer();
		TapGestureRecognizer TapPensador = new TapGestureRecognizer();
		TapGestureRecognizer TapCitasCel = new TapGestureRecognizer();
		TapGestureRecognizer TapBibleGat = new TapGestureRecognizer();
		public Frases()
		{
			InitializeComponent();
			Titulo.Text="Portal de Frases DataBase";
			Intro.Text="Aqui puede administrar sus frases o citas Biblicas, escribiendo, "+
			"o bien copiando y pegando, para ello puede visitar los enlaces en colores "+
			"mediante su conexión a internet";
			Titulo.HorizontalTextAlignment= TextAlignment.Center;
			Proverbia = new Label
			{
				Text = "PROVERBIA",
				HorizontalTextAlignment=TextAlignment.Center,
				TextDecorations= TextDecorations.Underline,
				Padding= new Thickness(20,0,10,10),
				FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
				TextColor=Color.Blue,
				FontAttributes= FontAttributes.Bold,
			};
			LayFrases.Children.Add(Proverbia);
			Pensador = new Label
			{
				Text = "PENSADOR",
				HorizontalTextAlignment=TextAlignment.Center,
				TextDecorations= TextDecorations.Underline,
				Padding= new Thickness(20,0,10,10),
				FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
				TextColor=Color.GreenYellow,
				FontAttributes= FontAttributes.Bold,
			};
			LayFrases.Children.Add(Pensador);
			MDFrases = new Label
			{
				Text = "Mundi Frases",
				HorizontalTextAlignment=TextAlignment.Center,
				TextDecorations= TextDecorations.Underline,
				Padding= new Thickness(20,0,10,10),
				FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
				TextColor=Color.Magenta,
				FontAttributes= FontAttributes.Bold,
			};
			LayFrases.Children.Add(MDFrases);
			CitaCel = new Label
			{
				Text = "CitasCélebres.com",
				HorizontalTextAlignment=TextAlignment.Center,
				TextDecorations= TextDecorations.Underline,
				Padding= new Thickness(20,0,10,10),
				FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
				TextColor=Color.Orange,
				FontAttributes= FontAttributes.Bold,
			};
			LayFrases.Children.Add(CitaCel);
			Biblia = new Label
			{
				Text = "BibleGateway.com",
				HorizontalTextAlignment=TextAlignment.Center,
				TextDecorations= TextDecorations.Underline,
				Padding= new Thickness(20,0,10,10),
				FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
				TextColor=Color.LightBlue,
				FontAttributes= FontAttributes.Bold,
			};
			LayFrases.Children.Add(Biblia);
			
			TapProverbia.Tapped += OnProverbiaTapped;
			async void OnProverbiaTapped (object sender, EventArgs args)
			{
				await Browser.OpenAsync("https://proverbia.net/", BrowserLaunchMode.SystemPreferred);
			}
			Proverbia.GestureRecognizers.Add(TapProverbia);
			
			TapMundiFra.Tapped += OnMundiFTapped;
			async void OnMundiFTapped (object sender, EventArgs args)
			{
				await Browser.OpenAsync("https://www.mundifrases.com/", BrowserLaunchMode.SystemPreferred);
			}
			MDFrases.GestureRecognizers.Add(TapMundiFra);
			
			TapPensador.Tapped += OnPensadorTapped;
			async void OnPensadorTapped (object sender, EventArgs args)
			{
				await Browser.OpenAsync("https://www.pensador.com/es/frases_del_dia/", BrowserLaunchMode.SystemPreferred);
			}
			Pensador.GestureRecognizers.Add(TapPensador);
			
			TapCitasCel.Tapped += OnCitaCelTapped;
			async void OnCitaCelTapped (object sender, EventArgs args)
			{
				await Browser.OpenAsync("https://www.citascelebres.com/", BrowserLaunchMode.SystemPreferred);
			}
			CitaCel.GestureRecognizers.Add(TapCitasCel);
			
			TapBibleGat.Tapped += OnBibleGatTapped;
			async void OnBibleGatTapped (object sender, EventArgs args)
			{
				await Browser.OpenAsync("https://www.biblegateway.com/", BrowserLaunchMode.SystemPreferred);
			}
			Biblia.GestureRecognizers.Add(TapBibleGat);
		}
		protected override async void OnAppearing()
		{
			base.OnAppearing();
			frasesView.ItemsSource = await App.FraseDataBase.GetFrasesAsync();
		}
		
		async void OnFraseTapped(object sender, EventArgs e)
		{
			try
			{
				string opcion = await DisplayActionSheet("Confirme:", "Borrar", "Editar", "Cancelar" );
				switch(opcion)
				{
					case "Borrar":
					{
						var lay = (BindableObject)sender;
						var item = (Frase)lay.BindingContext;
						bool result = await DisplayAlert("Confirmar", $"¿ Seguro quiere borrar {item.Texto} ?", "Si, borrar", "Cancelar");
						if(result)
						{
							await App.FraseDataBase.DeleteFraseAsync(item);
							(Application.Current).MainPage = new MainPage();
						}
					}
					break;
					case "Editar":
					{
						
						var data = (BindableObject)sender;
						var item = (Frase)data.BindingContext;
						count = (item.Texto).Length;
						
						editor.Text = item.Texto;
						entryAutor.Text = (item.Autor).Substring(2);
						
					}
					break;
					default: 
					{
						goto endTapf;
					}
						
						
					
				}
			}
			catch(Exception ex)
			{
				await Task.Delay(10);
				await DisplayAlert("Error de asignación", "", "Salir");
			}
			
			endTapf:;
		}
		public void OnEditorTextChanged(object sender, TextChangedEventArgs e)
		{
			if((editor.Text).Length != count)
			{	
				bool validText = !string.IsNullOrWhiteSpace(e.NewTextValue);
				update.IsEnabled = validText;
			}
		}
		private async void UpdateClick(object sender, EventArgs e)
		{
			await App.FraseDataBase.GuardarFraseAsync(new Frase
			{
				Texto = editor.Text, 
				Autor = entryAutor.Text
			});
			update.IsEnabled = false;
			editor.Text = string.Empty;
			entryAutor.Text = string.Empty;
			frasesView.ItemsSource = await App.FraseDataBase.GetFrasesAsync();
		}
		private async void OnSaveFraseClick(object sender, EventArgs e)
		{
			if(!string.IsNullOrEmpty(editor.Text))
			{
				await App.FraseDataBase.SaveFraseAsync(new Frase
				{
					Texto = editor.Text,
					Autor = "- " + entryAutor.Text
				});
				editor.Text = string.Empty;
				entryAutor.Text = string.Empty;
				frasesView.ItemsSource = await App.FraseDataBase.GetFrasesAsync();
			}
		}
	}
}
