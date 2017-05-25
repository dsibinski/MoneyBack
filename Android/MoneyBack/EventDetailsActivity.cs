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
        }

        protected override void OnResume()
        {
            base.OnResume();
            InitializeUserControlsEvents();
        }

        private void InitializeUserControlsEvents()
        {
            _btnSaveEvent.Click += _btnSaveEvent_Click;
            _btnSelectDate.Click += _btnSelectDate_Click;
        }

        private void _btnSelectDate_Click(object sender, EventArgs e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                _selectedDate = time;
                _btnSelectDate.Text = _selectedDate.ToLongDateString();
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
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

            var @event = new Event
            {
                Name = name,
                Place = place,
                Date = _selectedDate
            };

            InsertEvent(@event);

            if (@event.Id == 0)
                Toast.MakeText(this, $"Event: Name={name} wasn't properly saved!", ToastLength.Long).Show();
            else
                Toast.MakeText(this, $"Event saved, details: {@event}", ToastLength.Long).Show();

            this.Finish();

        }

        private void InsertEvent(Event @event)
        {
            _dbContext.Events.Insert(@event);
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