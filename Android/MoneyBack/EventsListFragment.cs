using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MoneyBack.Entities;
using MoneyBack.Orm;

namespace MoneyBack
{
    public class EventsListFragment : ListFragment
    {
        private List<Event> _eventsList;

        private readonly DatabaseContext _dbContext = new DatabaseContext();


        public override void OnStart()
        {
            base.OnStart();
            InitializeEventsList();
        }

        protected void InitializeEventsList()
        {
            _eventsList = GetEvents();

            this.ListAdapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleListItem1,
                _eventsList.ConvertAll(p => p.ToString()));
        }

        public override void OnResume()
        {
            base.OnResume();
            InitializeUserControlsEvents();
        }

        public override void OnPause()
        {
            base.OnPause();
            DetatchUserControlsEvents();
        }

        private void InitializeUserControlsEvents()
        {
            this.ListView.ItemClick += ListView_ItemClick;
        }

        private void DetatchUserControlsEvents()
        {
            this.ListView.ItemClick -= ListView_ItemClick;
        }

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var @event = _eventsList[e.Position];

            var intent = new Intent(Application.Context, typeof(EventDetailsActivity));
            intent.PutExtra("dataSourceId", @event.Id);

            StartActivity(intent);
        }

        private List<Event> GetEvents()
        {

            var events = _dbContext.Events.GetAll();

            return events.ToList();
        }


    }
}