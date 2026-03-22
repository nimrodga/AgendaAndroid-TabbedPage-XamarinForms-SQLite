using System;
using SQLite;
using System.Linq;
using System.Collections.Generic;

namespace AgendaAndroid
{
    public class Person 
    {         
        [PrimaryKey, AutoIncrement]
        public int Id {get; set;}
        public string Name {get; set;}
        public bool Subscribed {get; set;}
    }
}
