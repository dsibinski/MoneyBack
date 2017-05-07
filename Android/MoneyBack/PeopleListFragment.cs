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
    public class PeopleListFragment : ListFragment
    {
        private List<Person> _peopleList;

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            InitializePeopleList();
        }

        public override void OnStart()
        {
            base.OnStart();
            InitializePeopleList();
        }

        protected async void InitializePeopleList()
        {
            _peopleList = await GetPeopleAsync();

            this.ListAdapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleExpandableListItem1, _peopleList.ConvertAll(p => p.ToString()));

            InitializeUserControlsEvents();
        }


        private async Task<List<Person>> GetPeopleAsync()
        {
            var repo = new Repository<Person>();

            var people = await repo.GetAll();

            return people.ToList();
        }

        private void InitializeUserControlsEvents()
        {
            this.ListView.ItemClick += ListView_ItemClick;
        }

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var person = _peopleList[e.Position];

            var uri = Android.Net.Uri.Parse("tel:" + person.PhoneNumber);

            var intent = new Intent(Intent.ActionDial, uri);

            StartActivity(intent);
        }
    }
}