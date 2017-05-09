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
using SQLite;

namespace MoneyBack.Entities
{
    [Table("Events")]
    public class Event
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Place { get; set; }

        public override string ToString()
        {
            return $"Event Name: {Name}\nDate: {Date}\nPlace: {Place}";
        }
    }
}