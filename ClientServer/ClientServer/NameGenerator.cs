using System;
using System.Collections.Generic;

namespace Server
{
    public class NameGenerator
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void Generate()
        {
            this.Name = new Bogus.Person().UserName;
            this.Id = Name.GetHashCode();
        }
    }
}