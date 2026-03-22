using System;
using SQLite;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AgendaAndroid
{
    public class FraseDataBase 
    {         
		private readonly SQLiteAsyncConnection _frasedatabase;
		
		public FraseDataBase(string dbfPath)
    	{
        	_frasedatabase = new SQLiteAsyncConnection(dbfPath);
        	_frasedatabase.CreateTableAsync<Frase>().Wait();
    	}
    	
		public Task <List<Frase>> GetFrasesAsync()
        {
        	return _frasedatabase.Table<Frase>().ToListAsync();
        }
    	
    	public Task<int> SaveFraseAsync(Frase frase)
        {
        	return _frasedatabase.InsertAsync(frase);
        	
        }
        public Task <int> DeleteFraseAsync(Frase frase)
		{
			return _frasedatabase.DeleteAsync(frase);
		}
		
		
		public async Task<Frase> GetRandomFraseAsync()
    	{
        	var lista = await _frasedatabase.Table<Frase>().ToListAsync();
        	if (lista.Count == 0) return null;
        	return lista[new Random().Next(lista.Count)];
    	}
    	public Task<Frase> GetFraseAleatoriaAsync()
    	{
    		return _frasedatabase.Table<Frase>().OrderBy(x => Guid.NewGuid()).FirstOrDefaultAsync();
    	}
    	public Task<int> GuardarFraseAsync(Frase frase)
    	{
    		if (frase.Id != 0) return _frasedatabase.UpdateAsync(frase);
    		else return _frasedatabase.InsertAsync(frase);
    	}
    }
}
