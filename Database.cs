using System;
using SQLite;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AgendaAndroid
{
    public class Database 
    {         
        private readonly SQLiteAsyncConnection _database;
        
        public Database (string dbpath)
        {
        	_database = new SQLiteAsyncConnection(dbpath);
        	_database.CreateTableAsync<Person>();
        }
        public Task <List<Person>> GetPeopleAsync()
        {
        	return _database.Table<Person>().ToListAsync();
        }
        public Task<int> SavePersonAsync(Person person)
        {
        	return _database.InsertAsync(person);
        	
        }
        public Task <int> DeletePersonAsync(Person person)
		{
			return _database.DeleteAsync(person);
		}
		public async Task <int> ToggleItemStatusAsync(Person person)
		{
			person.Subscribed = !person.Subscribed;
			return await _database.UpdateAsync(person);
		}
		
    }
}
