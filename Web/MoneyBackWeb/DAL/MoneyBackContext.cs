using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoneyBackWeb.Models;

namespace MoneyBackWeb.DAL
{
    public class MoneyBackContext : DbContext
    {
        // default ctor for StartUp where ConnectionString is injected automatically
        public MoneyBackContext() { }

        // will be used by unit tests - options are passed manually
        public MoneyBackContext(DbContextOptions options) : base(options) { }
       
        public DbSet<Person> People { get; set; }
    }
}
