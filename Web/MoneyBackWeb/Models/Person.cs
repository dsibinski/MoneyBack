using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoneyBackWeb.DAL;

namespace MoneyBackWeb.Models
{
    public class Person : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

    }
}
