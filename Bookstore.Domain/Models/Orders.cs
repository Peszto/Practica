using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Models {
    public class Orders : Entity
    {
        public int OrderNr { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public int BookId { get; set; }
        public int ClientId { get; set; }
        public Book Book { get; set; }
        public Client Client { get; set; }

    }
}
