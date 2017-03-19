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
using MoneyBack.Entities;
using MoneyBack.Helpers;
using SQLite;

namespace MoneyBack.Orm
{
    public class PeopleRepository
    {
        private SQLiteConnection db = null;
        protected static PeopleRepository me;

        static PeopleRepository()
        {
            me = new PeopleRepository();
        }

        protected PeopleRepository()
        {
            db = new SQLiteConnection(Constants.DbFilePath);
            db.CreateTable<Person>();
        }

        public static int SavePerson(Person person)
        {
            me.db.Insert(person);
            return person.Id;
        }

        public static Person GetPerson(int id)
        {
            return me.db.Get<Person>(p => p.Id == id);
        }
    }
}