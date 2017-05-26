using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MoneyBack.Orm;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Attributes;
using SQLite.Net.Interop;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensionsAsync.Extensions;

namespace MoneyBack.Entities
{
    [Table("Events")]
    public class Event : IEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Place { get; set; }

        [ManyToMany(typeof(PersonEvent))]
        public List<Person> Participants { get; set; }

        public override string ToString()
        {
            return $"Event Name: {Name}\nDate: {Date.ToShortDateString()}\nPlace: {Place}";
        }
    }
}