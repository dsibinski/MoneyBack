using System;
using System.Globalization;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using MoneyBack.Entities;
using MoneyBack.Helpers;
using MoneyBack.Orm;
using SQLite;

namespace MoneyBack
{
    [Activity(Label = "Money Back", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Button _btnPeople;
  

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            InitializeUserControls();
        }

        protected override void OnStart()
        {
            base.OnStart();
        }

        protected override void OnResume()
        {
            base.OnResume();
            InitializeUserControlsEvents();
        }

        protected override void OnPause()
        {
            base.OnPause();
            DetatchUserControlsEvents();
        }

        private void DetatchUserControlsEvents()
        {
            _btnPeople.Click -= _btnPeople_Click;
        }

        protected override void OnStop()
        {
            base.OnStop();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        private void InitializeUserControls()
        {
            _btnPeople = this.FindViewById<Button>(Resource.Id.btnPeople);
        }

        private void InitializeUserControlsEvents()
        {
            _btnPeople.Click += _btnPeople_Click;

        }

        private void _btnPeople_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(PeopleActivity));
            StartActivity(intent);
        }
    }
}