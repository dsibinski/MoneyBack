using System;
using System.Collections.Generic;
using MoneyBack.Orm;
using SQLite;
using SQLiteNetExtensions.Attributes;

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