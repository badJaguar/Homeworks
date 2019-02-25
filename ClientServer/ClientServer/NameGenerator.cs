using System;
using System.Collections.Generic;

namespace Server
{
    /// <summary>
    /// Represents random user names generation using Bogus NuGet package.
    /// </summary>
    public class NameGenerator
    {
        /// <summary>
        /// Gets or sets a unique ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a unique user name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Generates user names and Ids. ID is a hash code of random chosen name.
        /// </summary>
        public void Generate()
        {
            this.Name = new Bogus.Person().UserName;
            this.Id = Name.GetHashCode();
        }
    }
}