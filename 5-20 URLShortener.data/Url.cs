using System;

namespace _5_20_URLShortener.data
{
    public class Url
    {
        public int Id { get; set; }
        public string OrgUrl { get; set; }
        public string HashedUrl { get; set; }
        public int Views { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
