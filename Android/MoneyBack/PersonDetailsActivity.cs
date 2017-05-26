using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Java.IO;
using Java.Security;
using MoneyBack.Entities;
using MoneyBack.Helpers;
using MoneyBack.Orm;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Platform.XamarinAndroid;
using SQLiteNetExtensions.Extensions;

namespace MoneyBack
{
    [Activity(Label = "Person")]
    public class PersonDetailsActivity : Activity
    {
        private readonly DatabaseContext _dbContext = new DatabaseContext();

        private Person CurrentPerson { get; set; }
        private bool IsAddMode { get; set; }

        private Button _btnSavePerson;

        private EditText _inputName;
        private EditText _inputLastName;
        private EditText _inputPhoneNumber;
        private EditText _inputEmail;

        


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.PersonDetails);

            InitializeUserControls();

            InitDataSource();
        }

        private void InitDataSource()
        {
            var passedDataSourceId = Intent.Extras?.GetInt("dataSourceId");

            if (passedDataSourceId.HasValue && passedDataSourceId.Value > 0)
            {
                var id = passedDataSourceId.Value;
                CurrentPerson = _dbContext.People.GetWithChildren(id);
                if (CurrentPerson == null)
                    throw new ArgumentException($"Person with ID {id} was not found!");

                _inputName.Text = CurrentPerson.Name;
                _inputLastName.Text = CurrentPerson.LastName;
                _inputEmail.Text = CurrentPerson.Email;
                _inputPhoneNumber.Text = CurrentPerson.PhoneNumber;
            }
            else
            {
                CurrentPerson = new Person();
                IsAddMode = true;
            }
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
            _btnSavePerson.Click -= _btnSavePerson_Click;
        }


        private void InitializeUserControlsEvents()
        {
            _btnSavePerson.Click += _btnSavePerson_Click;
        }

        private void _btnSavePerson_Click(object sender, EventArgs e)
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

            CurrentPerson.Name = name;
            CurrentPerson.LastName = lastName;
            CurrentPerson.PhoneNumber = phoneNumber;
            CurrentPerson.Email = email;

            Save();

            if (CurrentPerson.Id == 0)
                Toast.MakeText(this, $"Person: Name={name}, LastName={lastName} wasn't properly saved!", ToastLength.Long).Show();
            else
                Toast.MakeText(this, $"Person saved, details: {CurrentPerson}", ToastLength.Long).Show();

            this.Finish();

        }

        private void Save()
        {
            if (IsAddMode)
                _dbContext.People.Insert(CurrentPerson);
            else
                _dbContext.People.UpdateWithChildren(CurrentPerson);
        }

        private void ValidateMandatoryFields()
        {
            if (String.IsNullOrEmpty(_inputName.Text))
                throw new ArgumentNullException($"Name", "Mandatory field cannot be empty!");

            if (String.IsNullOrEmpty(_inputEmail.Text))
                throw new ArgumentNullException($"Email", "Mandatory field cannot be empty!");
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