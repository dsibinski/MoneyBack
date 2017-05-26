using System;
using SQLiteNetExtensions.Attributes;

namespace MoneyBack.Entities
{
    [Serializable]
    public class PersonEvent
    {
        [ForeignKey(typeof(Person))]
        public int PersonId { get; set; }

        [ForeignKey(typeof(Event))]
        public int EventId { get; set; }
    }
}