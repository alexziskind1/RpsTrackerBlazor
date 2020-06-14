using Newtonsoft.Json;
using RPS.Core.Models;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace RPS.Data
{
    public class PtInMemoryContext
    {
        string resourceNameItems = "RPS.Data.Standard.GenData.fs-items.json";
        string resourceNameUsers = "RPS.Data.Standard.GenData.fs-users.json";

        List<PtItem> items;
        List<PtUser> users;

        public List<PtItem> PtItems { get { return items; } }
        public List<PtUser> PtUsers { get { return users; } }

        public PtInMemoryContext()
        {
            var assembly = Assembly.GetExecutingAssembly();

            string contentsItems = "[]";

            using (Stream stream = assembly.GetManifestResourceStream(resourceNameItems))
            using (StreamReader file = new StreamReader(stream))
            {
                contentsItems = file.ReadToEnd();
            }

            items = JsonConvert.DeserializeObject<List<PtItem>>(contentsItems);

            string contentsUsers = "[]";

            using (Stream stream = assembly.GetManifestResourceStream(resourceNameUsers))
            using (StreamReader file = new StreamReader(stream))
            {
                contentsUsers = file.ReadToEnd();
            }

            users = JsonConvert.DeserializeObject<List<PtUser>>(contentsUsers);
        }
    }
}