﻿using System.Collections.Generic;
using System.Linq;
using KitapeviStokTakipWebApi.Models;

namespace WebApi.Helpers
{
    public static class BasicAuthMethods
    {
        public static IEnumerable<User> WithoutPasswords(this IEnumerable<User> users)
        {
            return users.Select(x => x.WithoutPassword());
        }

        public static User WithoutPassword(this User user)
        {
            user.Password = null;
            return user;
        }
    }
}