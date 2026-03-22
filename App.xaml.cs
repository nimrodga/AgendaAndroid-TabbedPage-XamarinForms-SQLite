using System;
using System.IO;
using SQLite;
using Xamarin.Forms;
using System.Collections.Generic

namespace AgendaAndroid
{
	public partial class App : Application
	{
		private static Database database;
		private static FraseDataBase fraseDataBase;
		
		public static Database Database
		{
			get
			{
				if(database == null)
				{
					database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "people.db3" ));
				}
				return database;
			}
		}
		public static FraseDataBase FraseDataBase
		{
			get
			{
				if(fraseDataBase == null)
				{
					//Fijación de ruta estándar para bases de datos SQLite en Android Xamarin Forms
					//string dbfPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "citas.db3");
                	fraseDataBase = new FraseDataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "citas.db3"));
				}
				return fraseDataBase;
			}
		}
		public App()
		{
			InitializeComponent();
			MainPage = new MainPage();
		}
	}
}
