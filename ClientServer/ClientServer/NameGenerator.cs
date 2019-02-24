using System;
using System.Collections.Generic;

namespace Server
{
    public class NameGenerator
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public Dictionary<int,string> Dictionary { get; set; }

        public void Generate()
        {
            this.Name = new Bogus.Person().UserName;
            this.Id = Name.GetHashCode();
        }

        public string WriteNames()
        {
            var names = new Dictionary<int, string>();
            names.Add(Id, Name);
            return string.Join(" ", names.Values);
        }
    }
}