using NUnit.Framework;
using MoneyBack.Entities;
using MoneyBack.Orm;
using SQLite;

namespace MoneyBack.Android.Tests
{
    [TestFixture]
    public class RepositoryTests
    {
        public SQLiteAsyncConnection InMemorySqliteConnection;

        [OneTimeSetUp]
        public void Init()
        {
            InMemorySqliteConnection = new SQLiteAsyncConnection(":memory:");
        }


        [Test]
        public void one_new_person_inserted_adds_one_new_row()
        {
            // given
            var person = new Person()
            {
                Name = "A",
                LastName = "B"
            };

            var repo = new Repository<Person>(InMemorySqliteConnection);

            // when
            var numRows = repo.Insert(person).Result;
            
            // then
            Assert.AreEqual(1, numRows);
        }

        [Test]
        public void new_person_added_has_id_primarykey_generated()
        {
            // given
            var person1 = new Person
            {
                Name = "A",
                LastName = "B"
            };

            var person2 = new Person
            {
                Name = "A",
                LastName = "B"
            };

            var repo = new Repository<Person>(InMemorySqliteConnection);

            // when
            var n1 = repo.Insert(person1).Result; // getting Result in order to force Task's completion before continuing
            var n2 = repo.Insert(person2).Result;

            // then
            Assert.Greater(person1.Id, 0);
            Assert.AreEqual(person2.Id, person1.Id + 1);
        }
    }
}
