using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETFundamentals.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public bool Published { get; set; }
    }
}