using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MattTech.SlackToBookmarks.Models
{
    public class Message
    {
        public string Type { get; set; }
        public string User { get; set; }
        public string Text { get; set; }
        public IList<Attachment> Attachments { get; set; }
        public string Ts { get; set; }
    }
}
