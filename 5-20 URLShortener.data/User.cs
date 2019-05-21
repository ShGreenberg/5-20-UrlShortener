using System;
using System.Collections.Generic;
using System.Text;

namespace _5_20_URLShortener.data
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }

        public IEnumerable<Url> Urls { get; set; }
    }
}
