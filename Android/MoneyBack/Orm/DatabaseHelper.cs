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
using SQLite.Net;
using SQLite.Net.Async;

namespace MoneyBack.Orm
{
    public class DatabaseHelper
    {
        public static SQLiteAsyncConnection GetAndroidDbConnection(string dbFilePath)
        {
            var connectionFactory = new Func<SQLiteConnectionWithLock>(() =>
                new SQLiteConnectionWithLock(new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(),
                    new SQLiteConnectionString(dbFilePath, storeDateTimeAsTicks: true)));

            var newDb = new SQLiteAsyncConnection(connectionFactory);

            return newDb;
        }

    }
}