using System;
using System.Globalization;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Provider;
using MoneyBack.Entities;
using MoneyBack.Helpers;
using MoneyBack.Orm;
using SQLite;

namespace MoneyBack
{
    [Activity(Label = "MoneyBack", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private decimal _amount;
        private decimal _result;
        private int _numberOfPeople;

        private EditText _inputAmount;
        private EditText _inputNumberOfPeople;
        private EditText _txtResultDecimal;

        private EditText _inputName;
        private EditText _inputLastName;

        private Button _btnCalculate;
        private Button _btnAddPerson;

        

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            _amount = 0.00m;
            _result = 0.00m;
            _numberOfPeople = 0;


            InitializeUserControls();
        }

        protected override void OnStart()
        {
            base.OnStart();
            RefreshUserInputsFromVariables();
        }

        protected override void OnResume()
        {
            base.OnResume();
            InitializeUserControlsEvents();
            // TODO: retrieving variables (amount, number of people) from persistent storage (file, database)
        }

        protected override void OnPause()
        {
            base.OnPause();
            DetatchUserControlsEvents();
            // TODO: saving variables (amount, number of people) to persistent storage (file, database)
        }

        private void DetatchUserControlsEvents()
        {
            _btnCalculate.Click -= _btnCalculate_Click;
            _btnAddPerson.Click -= _btnAddPerson_Click;
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
            _inputAmount = this.FindViewById<EditText>(Resource.Id.inputAmount);
            _inputNumberOfPeople = this.FindViewById<EditText>(Resource.Id.inputNumberOfPeople);
            _txtResultDecimal = this.FindViewById<EditText>(Resource.Id.txtResultDecimal);
            _btnCalculate = this.FindViewById<Button>(Resource.Id.btnCalculate);
            _btnAddPerson = this.FindViewById<Button>(Resource.Id.btnAddPerson);

            _inputName = this.FindViewById<EditText>(Resource.Id.inputName);
            _inputLastName = this.FindViewById<EditText>(Resource.Id.inputLastName);
        }

        private void InitializeUserControlsEvents()
        {
            _btnCalculate.Click += _btnCalculate_Click;
            _btnAddPerson.Click += _btnAddPerson_Click;
        }

        private void _btnAddPerson_Click(object sender, EventArgs e)
        {
            var name = _inputName.Text;
            var lastName = _inputLastName.Text;

            var id = PeopleRepository.SavePerson(new Person
            {
                Name = name,
                LastName = lastName
            });

            var person = PeopleRepository.GetPerson(id);

            if (person == null)
                Toast.MakeText(this, $"Person: Name={name}, LastName={lastName} wasn't properly saved!", ToastLength.Long).Show();
            else
                Toast.MakeText(this, $"Person saved, details: {person}", ToastLength.Long).Show();

        }

        private void _btnCalculate_Click(object sender, System.EventArgs e)
        {
            RefreshVariablesFromUserInputs();
            CalculateResult();
            RefreshUserInputsFromVariables();
        }

        private void CalculateResult()
        {
            if (_numberOfPeople <= 0)
                Toast.MakeText(this, "Number of people must be greater than 0!", ToastLength.Long).Show();
            else
                _result = _amount / _numberOfPeople;
        }


        private void RefreshUserInputsFromVariables()
        {
            _inputAmount.SetText(_amount.ToString(CultureInfo.InvariantCulture), TextView.BufferType.Editable);
            _txtResultDecimal.SetText(_result.ToString(CultureInfo.InvariantCulture), TextView.BufferType.Editable);
            _inputNumberOfPeople.SetText(_numberOfPeople.ToString(CultureInfo.InvariantCulture),
                TextView.BufferType.Editable);
        }

        private void RefreshVariablesFromUserInputs()
        {
            _amount = Convert.ToDecimal(_inputAmount.Text, CultureInfo.InvariantCulture);
            _numberOfPeople = Convert.ToInt32(_inputNumberOfPeople.Text);
        }
    }
}