using System;
using System.Collections.Generic;
using Java.IO;
using NUnit.Framework;
using MoneyBack.Entities;
using MoneyBack.Helpers;
using MoneyBack.Orm;
using SQLite;
using SQLiteNetExtensions.Extensions;


namespace MoneyBack.Android.Tests
{
    [TestFixture]
    public class RepositoryTests
    {

         private readonly DatabaseContext _dbContext = new DatabaseContext(true);

         [Test]
         public void one_new_person_inserted_adds_one_new_row()
         {
             // given
             var person = new Person()
             {
                 Name = "A",
                 LastName = "B"
             };


             // when
             var rows = _dbContext.People.Insert(person);

             // then

             Assert.AreEqual(1, rows);
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


             // when
             var n1 = _dbContext.People.Insert(person1); // getting Result in order to force Task's completion before continuing
             var n2 = _dbContext.People.Insert(person2);
             var people = _dbContext.People.GetAll();
             // then
             Assert.Greater(person1.Id, 0);
             Assert.AreEqual(person2.Id, person1.Id + 1);
         }

         [Test]
         public void person_is_properly_saved_with_event()
         {
             var event1 = new Event
             {
                 Name = "Volleyball",
                 Date = new DateTime(2017, 06, 18),
                 Place = "Sports hall"
             };

             var person1 = new Person
             {
                 Name = "A",
                 LastName = "B",
                 PhoneNumber = "123456789"
             };


             // when
             var v1 = _dbContext.People.Insert(person1);
             var v2 = _dbContext.Events.Insert(event1);

             person1.Events = new List<Event> { event1 };
             _dbContext.People.UpdateWithChildren(person1);

             var personStored = _dbContext.People.GetWithChildren(person1.Id);
             var eventStored = _dbContext.Events.GetWithChildren(personStored.Events[0].Id);

             // then
             Assert.Greater(personStored.Id, 1);
             Assert.AreEqual(1, personStored.Events.Count);
             Assert.AreEqual(1, eventStored.Participants.Count);
         }

         [Test]
         public void two_persons_are_properly_saved_with_the_same_event()
         {
             // given
             var event1 = new Event
             {
                 Name = "Volleyball",
                 Date = new DateTime(2017, 06, 18),
                 Place = "Sports hall"
             };

             var person1 = new Person
             {
                 Name = "A",
                 LastName = "B",
                 PhoneNumber = "123456789"
             };

             var person2 = new Person
             {
                 Name = "B",
                 LastName = "C",
                 PhoneNumber = "333456789"
             };


             // when
             var v1 = _dbContext.People.Insert(person1);
             var v12 = _dbContext.People.Insert(person2);
             var v2 = _dbContext.Events.Insert(event1);

             person1.Events = new List<Event> { event1 }; // assign two different people to the same event
             person2.Events = new List<Event> { event1 };
             _dbContext.People.UpdateWithChildren(person1);
             _dbContext.People.UpdateWithChildren(person2);

             var personStored = _dbContext.People.GetWithChildren(person1.Id);
             var person2Stored = _dbContext.People.GetWithChildren(person2.Id);
             var eventStored = _dbContext.Events.GetWithChildren(personStored.Events[0].Id);

             // then
             Assert.Greater(personStored.Id, 1);
             Assert.Greater(person2Stored.Id, 1);
             Assert.AreEqual(1, personStored.Events.Count);
             Assert.AreEqual(1, person2Stored.Events.Count);
             Assert.AreEqual(personStored.Events[0].Id, person2Stored.Events[0].Id);
             Assert.AreEqual(2, eventStored.Participants.Count);
         }
    }
}