using Firebase.Database;
using Firebase.Storage;
using KingsCafeApp.LoginSystem;
using KingsCafeApp.Services;
using KingsCafeApp.Views;
using KingsCafeApp.Views.Admin;
using SQLite;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KingsCafeApp
{
    public partial class App : Application
    {

        public static String dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "dbKingsCafeApp.db3");
        public static SQLiteConnection db = new SQLiteConnection(dbPath);

        //Firebase Connections ======================================
        public static FirebaseStorage FirebaseStorage = new FirebaseStorage("gs://kingscafeapp.appspot.com");
        public static FirebaseClient firebaseDatabase = new FirebaseClient("https://kingscafeapp-default-rtdb.firebaseio.com/");
        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage= new StartupPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
