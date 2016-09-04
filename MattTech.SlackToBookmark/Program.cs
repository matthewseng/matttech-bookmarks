using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace MattTech.SlackToBookmarks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var unzipDirectory = Path.Combine(currentDirectory, "unzip");
            if (!Directory.Exists(unzipDirectory))
            {
                Directory.CreateDirectory(unzipDirectory);
            }

            var config = new ConfigurationBuilder()
                .SetBasePath(currentDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            var slackZipFile = config["SlackZipFile"];
            var outputBookmarkFile = config["OutputBookmarksFile"];

            if (File.Exists(slackZipFile))
            {
                var jsonParser = new JsonParser();

                if (!Directory.Exists(unzipDirectory))
                {
                    ZipFile.ExtractToDirectory(slackZipFile, unzipDirectory);
                }

                var messages = jsonParser.ParseDirectory(unzipDirectory);

                //var distinctTs = messages.Select(m => new { Test = m.Ts }).ToList().Distinct().Count();
                //var messagesWithMoreThanOneAttachments = messages.Where(m => m.Attachments.IsNotNullOrEmpty() && m.Attachments.Count > 1).ToList();
                //var messagesWithoutAttachments = messages.Where(m => m.Attachments.IsNullOrEmpty()).ToList();

                var converter = new MessageBookmarkConverter();
                var bookmarks = converter.Convert(messages);

                //var unconvertedMessages = messages.Where(m => !bookmarks.Select(b => b.Ts).Contains(m.Ts)).ToList();
            }
        }
    }
}
