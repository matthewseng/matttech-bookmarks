using MattTech.SlackToBookmarks.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MattTech.SlackToBookmarks
{
    public class JsonParser
    {
        public IList<Message> ParseDirectory(string directory)
        {
            IList<Message> messages = new List<Message>();

            List<string> subDirectories = Directory.GetDirectories(directory).ToList();
            if (subDirectories.IsNotNullOrEmpty())
            {
                foreach (var subDirectory in subDirectories)
                {
                    List<string> jsonFiles = Directory.GetFiles(subDirectory).ToList();
                    if (jsonFiles.IsNotNullOrEmpty())
                    {
                        foreach (var jsonFile in jsonFiles)
                        {
                            var tempMessages = ParseFile(jsonFile);
                            if (tempMessages.IsNotNullOrEmpty())
                            {
                                messages.AddRange(tempMessages);
                            }
                        }
                    }
                }
            }

            return messages;
        }

        public IList<Message> ParseFile(string jsonFile)
        {
            IList<Message> messages = null;

            using (StreamReader stream = File.OpenText(jsonFile))
            {                
                var jsonTextReader = new JsonTextReader(stream);
                var jsonSerializer = new JsonSerializer()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy()
                    }
                };

                messages = jsonSerializer.Deserialize<IList<Message>>(jsonTextReader);
            }

            return messages;
        }
    }
}
