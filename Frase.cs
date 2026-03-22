using System;
using SQLite;
using System.Linq;
using System.Collections.Generic;

namespace AgendaAndroid
{
    public class Frase 
    {         
		[PrimaryKey, AutoIncrement]
    	public int Id { get; set; }
    	public string Texto { get; set; }
    	public string Autor { get; set; }
    }
}
