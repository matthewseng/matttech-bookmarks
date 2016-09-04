using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MattTech.SlackToBookmarks.Models
{
    public class Attachment
    {
        public string Title { get; set; }
        public string TitleLink { get; set; }
        public string Text { get; set; }
        public string Fallback { get; set; }
        public string ThumbUrl { get; set; }
        public string FromUrl { get; set; }
        public int ThumbWidth { get; set; }
        public int ThumbHeight { get; set; }
        public string ServiceIcon { get; set; }
        public string ServiceName { get; set; }
        public int Id { get; set; }
    }
}
