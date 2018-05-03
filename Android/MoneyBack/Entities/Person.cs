using System;
using System.Collections.Generic;
using MoneyBack.Orm;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace MoneyBack.Entities
{
    [Serializable]
    [Table("People")]
    public class Person : IEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        [ManyToMany(typeof(PersonEvent))]
        public List<Event> Events { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}\nLast Name: {LastName}\nPhone Number: {PhoneNumber}\nEmail: {Email}";
        }
    }
}