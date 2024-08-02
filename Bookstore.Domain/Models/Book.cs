using System;

namespace BookStore.Domain.Models
{
    public class Book : Entity
    {
        public string Name {get;set;}
        public string Author { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime PublishDate { get; set; }
        public int CategoryId { get; set; }
        public int Pieces { get; set; }
        /* EF Relation */
        public Category Category { get; set; }

        public string FileName { get; set; }
    }
}