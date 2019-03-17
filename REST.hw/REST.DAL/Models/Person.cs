using System;

namespace REST.BLL.Models
{
    public class Person
    {
        /// <summary>
        /// Gets or sets a person ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a person name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a person lastname.
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets a person birth date.
        /// </summary>
        public DateTime BirthDate { get; set; }
    }
}