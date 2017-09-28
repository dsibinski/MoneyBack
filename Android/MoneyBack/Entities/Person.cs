using System;
using System.Collections.Generic;
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
    [Serializable]
    public class Person : RealmObject, IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public IList<Event> Events { get;  }

        public override string ToString()
        {
            return $"Name: {Name}\nLast Name: {LastName}\nPhone Number: {PhoneNumber}\nEmail: {Email}";
        }
    }
}