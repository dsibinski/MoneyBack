using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    [Activity(Label = "Event")]
    public class EventDetailsActivity : Activity
    {
        private readonly DatabaseContext _dbContext = new DatabaseContext();

        private Event CurrentEvent { get; set; }
        private bool IsAddMode { get; set; }

        private Button _btnSaveEvent;
        private Button _btnSelectDate;

        private EditText _inputName;
        private EditText _inputPlace;

        private DateTime _selectedDate;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.EventDetails);

            InitializeUserControls();

            InitDataSource();
        }

        private void InitDataSource()
        {
            var passedDataSourceId = Intent.Extras?.GetInt("dataSourceId");

            if (passedDataSourceId.HasValue && passedDataSourceId.Value > 0)
            {
                var id = passedDataSourceId.Value;
                CurrentEvent = _dbContext.Events.GetWithChildren(id);
                if (CurrentEvent == null)
                    throw new ArgumentException($"Event with ID {id} was not found!");

                _inputName.Text = CurrentEvent.Name;
                _inputPlace.Text = CurrentEvent.Place;
                _selectedDate = CurrentEvent.Date;
                _btnSelectDate.Text = _selectedDate.ToLongDateString();
            }
            else
            {
                CurrentEvent = new Event();
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

        private void InitializeUserControlsEvents()
        {
            _btnSaveEvent.Click += _btnSaveEvent_Click;
            _btnSelectDate.Click += _btnSelectDate_Click;
        }

        private void DetatchUserControlsEvents()
        {
            _btnSaveEvent.Click -= _btnSaveEvent_Click;
            _btnSelectDate.Click -= _btnSelectDate_Click;
        }

        private void _btnSelectDate_Click(object sender, EventArgs e)
        {
            new DatePickerFragment(delegate(DateTime time)
                {
                    _selectedDate = time;
                    _btnSelectDate.Text = _selectedDate.ToLongDateString();
                })
                .Show(FragmentManager, DatePickerFragment.TAG);
        }

        private void _btnSaveEvent_Click(object sender, EventArgs e)
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
            var place = _inputPlace.Text;

            CurrentEvent.Name = name;
            CurrentEvent.Place = place;
            CurrentEvent.Date = _selectedDate;

            Save();

            if (CurrentEvent.Id == 0)
                Toast.MakeText(this, $"Event: Name={name} wasn't properly saved!", ToastLength.Long).Show();
            else
                Toast.MakeText(this, $"Event saved, details: {CurrentEvent}", ToastLength.Long).Show();

            this.Finish();

        }

        private void Save()
        {
            if(IsAddMode)
                _dbContext.Events.Insert(CurrentEvent);
            else
                _dbContext.Events.UpdateWithChildren(CurrentEvent);
        }

        private void ValidateMandatoryFields()
        {
            if (String.IsNullOrEmpty(_inputName.Text))
                throw new ArgumentNullException($"Name", "Mandatory field cannot be empty!");
        }

        private void InitializeUserControls()
        {
            _btnSaveEvent = this.FindViewById<Button>(Resource.Id.btnSaveEvent);
            _btnSelectDate = this.FindViewById<Button>(Resource.Id.btnSelectEventDate);

            _inputName = this.FindViewById<EditText>(Resource.Id.inputEventName);
            _inputPlace = this.FindViewById<EditText>(Resource.Id.inputEventPlace);
        }
    }
}