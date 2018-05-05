using MoneyBack.Entities;
using MoneyBack.Helpers;
using SQLite;

namespace MoneyBack.Orm
{
    public class DatabaseContext
    {
        public IRepository<Event> Events { get; set; }
        public IRepository<Person> People { get; set; }
        public IRepository<PersonEvent> PersonEvents { get; set; }

        /// <summary>
        /// Creates new DatabaseContext.
        /// </summary>
        /// <param name="removeDbFileFirst">If set, removes database file before initializing the DB. Can be used for tests purposes.</param>
        public DatabaseContext(bool removeDbFileFirst = false)
        {
            if (removeDbFileFirst)
                JavaIoHelper.RemoveJavaFileIfExists(Constants.DbFilePath);

            InitializeTables(Constants.DbFilePath);
        }

        private void InitializeTables(string dbFilePath)
        {
            Events = new SqliteRepository<Event>(dbFilePath);
            People = new SqliteRepository<Person>(dbFilePath);
            PersonEvents = new SqliteRepository<PersonEvent>(dbFilePath);
        }



    }
}