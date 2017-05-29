using System;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Views;
using ActionBar = Android.App.ActionBar;

namespace MoneyBack
{
    [Activity(Label = "@string/ApplicationName", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private int _peopleTabPosition { get; set; }

        private int _eventsTabPosition { get; set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            InitializeTabs();

        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.ActionBarMenu, menu);
            return base.OnPrepareOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.menuAdd)
            {
                OpenAddingNewPerson();
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void InitializeTabs()
        {
            this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

            _peopleTabPosition = AddTab(GetString(Resource.String.peopleTabTitle), new PeopleListFragment());
            _eventsTabPosition = AddTab(GetString(Resource.String.eventsTabTitle), new EventsListFragment());
        }

        int AddTab(string tabText, Fragment view)
        {
            var tab = this.ActionBar.NewTab();
            tab.SetText(tabText);

            tab.TabSelected += delegate (object sender, ActionBar.TabEventArgs e)
            {
                var fragment = this.FragmentManager.FindFragmentById(Resource.Id.tabFragmentsContainer);
                if (fragment != null)
                    e.FragmentTransaction.Remove(fragment);
                e.FragmentTransaction.Add(Resource.Id.tabFragmentsContainer, view);
            };
            tab.TabUnselected += delegate (object sender, ActionBar.TabEventArgs e) {
                e.FragmentTransaction.Remove(view);
            };

            this.ActionBar.AddTab(tab);

            return tab.Position;
        }

        protected override void OnStart()
        {
            base.OnStart();
        }

        protected override void OnResume()
        {
            base.OnResume();

        }

        protected override void OnPause()
        {
            base.OnPause();
        }

        protected override void OnStop()
        {
            base.OnStop();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            var tabSelectedPosition = this.ActionBar.SelectedNavigationIndex;
            outState.PutInt("selectedTabPosition", tabSelectedPosition);

            base.OnSaveInstanceState(outState);
        }

        protected override void OnRestoreInstanceState(Bundle savedInstanceState)
        {
            base.OnRestoreInstanceState(savedInstanceState);

            var previouslySelectedTabPosition = savedInstanceState.GetInt("selectedTabPosition", 0);
            this.ActionBar.SetSelectedNavigationItem(previouslySelectedTabPosition);
        }


        private void OpenAddingNewPerson()
        {
            var selectedTabPosition = this.ActionBar.SelectedTab.Position;

            if (selectedTabPosition == _peopleTabPosition)
                OpenActivityIntent<PersonDetailsActivity>();
            else if (selectedTabPosition == _eventsTabPosition)
                OpenActivityIntent<EventDetailsActivity>();

        }

        private void OpenActivityIntent<T>() where T : Activity
        {
            var intent = new Intent(this, typeof(T));
            StartActivity(intent);
        }
    }
}