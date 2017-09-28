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
using Realms;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Attributes;
using SQLite.Net.Interop;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensionsAsync.Extensions;

namespace MoneyBack.Entities
{
    public class Event : RealmObject, IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Place { get; set; }

        public IList<Person> Participants { get; }

        public override string ToString()
        {
            return $"Event Name: {Name}\nDate: {Date.ToString("d")}\nPlace: {Place}";
        }
    }
}