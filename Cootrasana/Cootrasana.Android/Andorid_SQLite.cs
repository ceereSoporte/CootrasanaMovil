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
using Cootrasana.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(Cootrasana.Droid.Andorid_SQLite.Android_SQLite))]
namespace Cootrasana.Droid
{
    public class Andorid_SQLite
    {
        public class Android_SQLite : ConectionSQLite
        {
            public SQLite.SQLiteConnection GetConnection()
            {
                var dbName = "Tickets.sqlite";
                var dbPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
                var path = System.IO.Path.Combine(dbPath, dbName);
                var conn = new SQLite.SQLiteConnection(path);
                return conn;
            }
        }
    }
}