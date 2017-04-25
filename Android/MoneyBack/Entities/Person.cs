using SQLite;

namespace MoneyBack.Entities
{
    [Table("People")]
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public override string ToString()
        {
            return $"[Person: Id={Id}, Name={Name}, LastName={LastName}, PhoneNumber={PhoneNumber}, Email={Email}]";
        }
    }
}