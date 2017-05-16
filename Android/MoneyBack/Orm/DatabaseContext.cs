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
using SQLite.Net;
using SQLite.Net.Async;

namespace MoneyBack.Orm
{
    public class DatabaseContext
    {
        public Repository<Event> Events { get; set; }
        public Repository<Person> People { get; set; }
        public Repository<PersonEvent> PersonEvents { get; set; }

        public DatabaseContext()
        {
            var sqliteConnection = GetAndroidDbConnection(Constants.DbFilePath);

            Events = new Repository<Event>(sqliteConnection);
            People = new Repository<Person>(sqliteConnection);
            PersonEvents = new Repository<PersonEvent>(sqliteConnection);
        }

        private SQLiteAsyncConnection GetAndroidDbConnection(string dbFilePath)
        {
            var connectionFactory = new Func<SQLiteConnectionWithLock>(() =>
                new SQLiteConnectionWithLock(new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(),
                    new SQLiteConnectionString(dbFilePath, true)));

            var newDb = new SQLiteAsyncConnection(connectionFactory);

            return newDb;
        }



    }
}