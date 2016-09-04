using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MattTech.SlackToBookmarks.Models
{
    public class Bookmark
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public string Ts { get; set; }
    }
}
