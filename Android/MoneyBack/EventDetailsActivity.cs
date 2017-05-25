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
using MoneyBack.Orm;

namespace MoneyBack
{
    [Activity(Label = "Event")]
    public class EventDetailsActivity : Activity
    {
        private readonly DatabaseContext _dbContext = new DatabaseContext();
    }
}