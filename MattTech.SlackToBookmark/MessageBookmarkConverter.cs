using MattTech.SlackToBookmarks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MattTech.SlackToBookmarks
{
    public class MessageBookmarkConverter
    {
        public IList<Bookmark> Convert(IList<Message> messages)
        {
            if (messages == null)
            {
                throw new ArgumentNullException("messages");
            }

            IList<Bookmark> bookmarks = new List<Bookmark>();

            foreach (var message in messages)
            {
                var bookmark = new Bookmark();

                if (message.Attachments.IsNullOrEmpty())
                {
                    var messageText = message.Text;
                    var linkIndex = messageText.IndexOf("<http");
                    if (linkIndex > -1)
                    {
                        bookmark.Name = messageText.Substring(0, linkIndex).Trim();

                        var linkText = messageText.Substring(linkIndex);

                        var pattern = @"\<(.*?)\>";
                        var matches = Regex.Matches(linkText, pattern);
                        if (matches.Count > 0)
                        {
                            bookmark.Link = matches[0].Groups[1].Value;
                        }
                    }
                }
                else
                {
                    var attachment = message.Attachments.First();
                    bookmark.Name = attachment.Title;
                    bookmark.Link = attachment.TitleLink;
                }

                if (!string.IsNullOrEmpty(bookmark.Link))
                {
                    bookmark.Ts = message.Ts;
                    bookmarks.Add(bookmark);
                }
            }

            return bookmarks;
        }
    }
}
