using System;

namespace REST.Homework.Services.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual DateTime ReceiptDate { get; set; }
    }
}