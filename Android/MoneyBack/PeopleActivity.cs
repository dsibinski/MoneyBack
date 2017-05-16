using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Java.Security;
using MoneyBack.Entities;
using MoneyBack.Orm;

namespace MoneyBack
{
    [Activity(Label = "People")]
    public class PeopleActivity : Activity
    {
        private Button _btnSavePerson;

        private EditText _inputName;
        private EditText _inputLastName;
        private EditText _inputPhoneNumber;
        private EditText _inputEmail;

        private readonly DatabaseContext _dbContext = new DatabaseContext();


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
            
        }

        private async void _btnSavePerson_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateMandatoryFields();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
                return;
            }
            

            var name = _inputName.Text;
            var lastName = _inputLastName.Text;
            var phoneNumber = _inputPhoneNumber.Text;
            var email = _inputEmail.Text;

            

            var person = new Person
            {
                Name = name,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                Email = email
            };

            await InsertPerson(person);

            if (person.Id == 0)
                Toast.MakeText(this, $"Person: Name={name}, LastName={lastName} wasn't properly saved!", ToastLength.Long).Show();
            else
                Toast.MakeText(this, $"Person saved, details: {person}", ToastLength.Long).Show();

            this.Finish();

        }

        private void ValidateMandatoryFields()
        {
            if (String.IsNullOrEmpty(_inputName.Text))
                throw new ArgumentNullException($"Name", "Mandatory field cannot be empty!");

            if (String.IsNullOrEmpty(_inputEmail.Text))
                throw new ArgumentNullException($"Email", "Mandatory field cannot be empty!");
        }

        private async Task InsertPerson(Person person)
        {
            await _dbContext.People.Insert(person);
        }

        protected override void OnPause()
        {
            base.OnPause();
            DetatchUserControlsEvents();
        }

        private void DetatchUserControlsEvents()
        {
            _btnSavePerson.Click -= _btnSavePerson_Click;
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

            _inputName = this.FindViewById<EditText>(Resource.Id.inputName);
            _inputLastName = this.FindViewById<EditText>(Resource.Id.inputLastName);
            _inputPhoneNumber = this.FindViewById<EditText>(Resource.Id.inputPhoneNumber);
            _inputEmail = this.FindViewById<EditText>(Resource.Id.inputEmail);

        }
    }
}