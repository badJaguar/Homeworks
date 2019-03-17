using System;
using FluentValidation;

namespace REST.BLL.Models
{
    /// <summary>
    /// Represents a request for creating and updating a person.
    /// </summary>
    public class UpdatePersonRequest
    {
        /// <summary>
        /// Gets or sets a person name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a person lastname.
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets a person date of birth.
        /// </summary>
        public DateTime BirthDate { get; set; }
    }
}