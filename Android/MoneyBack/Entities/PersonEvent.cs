using SQLiteNetExtensions.Attributes;

namespace MoneyBack.Entities
{
    public class PersonEvent
    {
        [ForeignKey(typeof(Person))]
        public int PersonId { get; set; }

        [ForeignKey(typeof(Event))]
        public int EventId { get; set; }
    }
}