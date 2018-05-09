using CoreWebAPI.EF;
using CoreWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebAPI
{
    public static class DbInitializer
    {
        public static void Initialize(DataContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Courier.Any())
            {
                return;   // DB has been seeded
            }

            var students = new Courier[]
            {
                new Courier{ Name = "Kek", BirthDate = new DateTime(2005, 10, 18), Login = "kek@mail.kekus", Password = "qweqwe", PhotoUrl="kek.ru", Rate=2.5f }
            };
            foreach (Courier s in students)
            {
                context.Courier.Add(s);
            }
            context.SaveChanges();
        }
    }
}
