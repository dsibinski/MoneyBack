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

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            InitializeEventsList();
        }

        public override void OnStart()
        {
            base.OnStart();
            InitializeEventsList();
        }

        protected async void InitializeEventsList()
        {
            _eventsList = await GetEventsAsync();

            this.ListAdapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleListItem1,
                _eventsList.ConvertAll(p => p.ToString()));

        }


        private async Task<List<Event>> GetEventsAsync()
        {

            var events = _dbContext.Events.GetAll();

            return events.ToList();
        }


    }
}