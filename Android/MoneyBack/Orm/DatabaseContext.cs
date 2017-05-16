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
using Java.IO;
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

        /// <summary>
        /// Creates new DatabaseContext.
        /// </summary>
        /// <param name="removeDbFileFirst">If set, removes database file before initializing the DB. Can be used for tests purposes.</param>
        public DatabaseContext(bool removeDbFileFirst = false)
        {
            if (removeDbFileFirst)
                JavaIoHelper.RemoveJavaFileIfExists(Constants.DbFilePath);

            var sqliteConnection = GetAndroidDbConnection(Constants.DbFilePath);

            InitializeTables(sqliteConnection);
        }

        private void InitializeTables(SQLiteAsyncConnection sqliteConnection)
        {
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