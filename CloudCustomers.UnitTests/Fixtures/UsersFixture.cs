using CloudCustomersAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudCustomers.UnitTests.Fixtures
{
    public static class UsersFixture
    {
        public static List<User> GetTestUsers() =>
            new()
            {
                new User
                {
                    Name = "Bill",
                    Address = new Address()
                    {
                        City = "Leeds",
                        Street = "Nice Street",
                        ZipCode = "BH18 4AG"
                    },
                    Email = "Bill@gmail.com"
                },
                new User
                {
                    Name = "Ben",
                    Address = new Address()
                    {
                        City = "Manchester",
                        Street = "Great Street",
                        ZipCode = "SK8 4AG"
                    },
                    Email = "Ben@gmail.com"
                },
                new User
                {
                    Name = "Hugo",
                    Address = new Address()
                    {
                        City = "Glasgow",
                        Street = "Cold Street",
                        ZipCode = "GL 4AF"
                    },
                    Email = "Hugo@hotmail.com"
                }
            };
    }
}
