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
    [Activity(Label = "@string/peopleListTitle")]
    public class PeopleListActivity : ListActivity
    {
        private List<Person> _peopleList;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var repo = new Repository<Person>();

            _peopleList = await GetPeopleAsync();

            this.ListAdapter = new ArrayAdapter<Person>(this, Android.Resource.Layout.SimpleListItem1, _peopleList);

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