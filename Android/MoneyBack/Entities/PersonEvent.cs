using System;
using MoneyBack.Orm;
using SQLiteNetExtensions.Attributes;

namespace MoneyBack.Entities
{
    [Serializable]
    public class PersonEvent : IEntity
    {
        [ForeignKey(typeof(Person))]
        public int PersonId { get; set; }

        [ForeignKey(typeof(Event))]
        public int EventId { get; set; }

        public int Id { get; set; }
    }
}