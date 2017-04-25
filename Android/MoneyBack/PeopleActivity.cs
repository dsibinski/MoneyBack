using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using MoneyBack.Entities;
using MoneyBack.Orm;

namespace MoneyBack
{
    [Activity(Label = "People")]
    public class PeopleActivity : Activity
    {
        private Button _btnSavePerson;
        private Button _btnPeopleList;

        private EditText _inputName;
        private EditText _inputLastName;
        private EditText _inputPhoneNumber;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.People);

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

        private void InitializeUserControlsEvents()
        {
            _btnSavePerson.Click += _btnSavePerson_Click;
            _btnPeopleList.Click += _btnPeopleList_Click;
            
        }

        private void _btnPeopleList_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(PeopleListActivity));
            StartActivity(intent);
        }

        private async void _btnSavePerson_Click(object sender, EventArgs e)
        {
            var name = _inputName.Text;
            var lastName = _inputLastName.Text;
            var phoneNumber = _inputPhoneNumber.Text;

            var person = new Person
            {
                Name = name,
                LastName = lastName,
                PhoneNumber = phoneNumber
            };

            var rows = await InsertPerson(person);

            if (rows <= 0)
                Toast.MakeText(this, $"Person: Name={name}, LastName={lastName} wasn't properly saved!", ToastLength.Long).Show();
            else
                Toast.MakeText(this, $"Person saved, details: {person}", ToastLength.Long).Show();

        }

        private async Task<int> InsertPerson(Person person)
        {
            var repo = new Repository<Person>();
            var rowsNumber = await repo.Insert(person);
            return rowsNumber;
        }

        protected override void OnPause()
        {
            base.OnPause();
            DetatchUserControlsEvents();
        }

        private void DetatchUserControlsEvents()
        {
            _btnSavePerson.Click -= _btnSavePerson_Click;
            _btnPeopleList.Click -= _btnPeopleList_Click;
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
            _btnSavePerson = this.FindViewById<Button>(Resource.Id.btnSavePerson);
            _btnPeopleList = this.FindViewById<Button>(Resource.Id.btnPeopleList);

            _inputName = this.FindViewById<EditText>(Resource.Id.inputName);
            _inputLastName = this.FindViewById<EditText>(Resource.Id.inputLastName);
            _inputPhoneNumber = this.FindViewById<EditText>(Resource.Id.inputPhoneNumber);
            
        }
    }
}