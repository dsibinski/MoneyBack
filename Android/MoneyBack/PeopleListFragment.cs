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
        private readonly DatabaseContext _dbContext = new DatabaseContext();

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

        protected void InitializePeopleList()
        {
            _peopleList = GetPeople();

            this.ListAdapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleListItem1, _peopleList.ConvertAll(p => p.ToString()));

            
        }

        public override void OnResume()
        {
            base.OnResume();
            InitializeUserControlsEvents();
        }

        private void InitializeUserControlsEvents()
        {
            this.ListView.ItemClick += ListView_ItemClick;
            this.ListView.ItemLongClick += ListView_ItemLongClick;
        }

        private void ListView_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            var menu = new PopupMenu(Application.Context, (View)sender);
            menu.Inflate(Resource.Menu.popupmenu);
            menu.MenuItemClick += (s, a) =>
            {
                switch (a.Item.ItemId)
                {
                    case Resource.Id.pm_edit:
                        Edit(e.Position);
                        break;
                    case Resource.Id.pm_delete:
                        Delete(e.Position);
                        InitializePeopleList();
                        break;
                }
            };
            menu.Show();
        }

        public override void OnPause()
        {
            base.OnPause();
            DetatchUserControlsEvents();
        }

        private void DetatchUserControlsEvents()
        {
            this.ListView.ItemClick -= ListView_ItemClick;
            this.ListView.ItemLongClick -= ListView_ItemLongClick;
        }

        private List<Person> GetPeople()
        {
            var people = _dbContext.People.GetAll();

            return people.ToList();
        }

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Edit(e.Position);
        }

        private void Edit(int listPosition)
        {
            var person = _peopleList[listPosition];

            var intent = new Intent(Application.Context, typeof(PersonDetailsActivity));
            intent.PutExtra("dataSourceId", person.Id);

            StartActivity(intent);
        }

        private void Delete(int listPosition)
        {
            var person = _peopleList[listPosition];

            _dbContext.People.Delete(person);
        }
    }
}