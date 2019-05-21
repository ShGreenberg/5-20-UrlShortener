using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using shortid;

namespace _5_20_URLShortener.data
{
    public class UrlRepository
    {
        private string _connString;
        public UrlRepository(string connString)
        {
            _connString = connString;
        }

        public Url Shorten(string url, string email = "")
        {
            using (var ctx = new UrlContext(_connString))
            {
                string hash = "";
                var urls = ctx.Urls;
                if (urls.FirstOrDefault(u => u.OrgUrl == url) == null)
                {
                    hash = "http://localhost:57845/" + ShortId.Generate(7);
                    while (urls.FirstOrDefault(u => u.HashedUrl == hash) != null)
                    {
                        hash = ShortId.Generate(7);
                    }
                    if (email != "")
                    {
                        ctx.Urls.Add(new Url
                        {
                            OrgUrl = url,
                            HashedUrl = hash,
                            UserId = GetUserByEmail(email).Id

                        });
                    }
                    else
                    {
                        ctx.Urls.Add(new Url
                        {
                            OrgUrl = url,
                            HashedUrl = hash
                        });
                    }
                    ctx.SaveChanges();
                }
                else
                {
                    hash = urls.FirstOrDefault(u => u.OrgUrl == url).HashedUrl;
                }
                return new Url { OrgUrl = url, HashedUrl = hash };

                }


        }

        private User GetUserByEmail(string email)
        {
            using (var ctx = new UrlContext(_connString))
            {
                return ctx.Users.FirstOrDefault(u => u.Email == email);
            }
        }

        public IEnumerable<Url> GetMyUrls(string email)
        {
            User user = GetUserByEmail(email);
            using (var ctx = new UrlContext(_connString))
            {
                return ctx.Urls.Where(u => u.UserId == user.Id).ToList();
            }
        }

        public Url GetUrl(string shortenUrl)
        {
            using(var ctx = new UrlContext(_connString))
            {
                var url = ctx.Urls.FirstOrDefault(u => u.HashedUrl == "http://localhost:57845/" + shortenUrl);
                if(url == null)
                {
                    return null;
                }
                ctx.Database.ExecuteSqlCommand("UPDATE Urls SET Views = views+1 WHERE id = @id",
                    new SqlParameter("@id", url.Id));
                return url;
            }
        }
    }
}
