using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _5_20_URLShortener.data
{
    public class UserRepository
    {
        private string _connString;
        public UserRepository(string connString)
        {
            _connString = connString;
        }

        public string AddUser(User user, string password)
        {
            user.HashedPassword = PasswordHelper.HashPassword(password);
            using (var ctx = new UrlContext(_connString))
            {
                if (ctx.Users.FirstOrDefault(u => u.Email == user.Email) != null)
                {
                    return "Email Already Used";
                }
                ctx.Users.Add(user);
                ctx.SaveChanges();
                return "";
            }
        }

        public User Login(string email, string password)
        {
            User user = GetUserByEmail(email);
            if (user == null)
            {
                return null;
            }
            if (PasswordHelper.PasswordMatch(password, user.HashedPassword))
            {
                return user;
            }
            return null;
        }

        private User GetUserByEmail(string email)
        {
            using (var ctx = new UrlContext(_connString))
            {
                return ctx.Users.FirstOrDefault(u => u.Email == email);
            }
        }
    }
}
