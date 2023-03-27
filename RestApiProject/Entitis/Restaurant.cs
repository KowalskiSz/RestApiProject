using System;
using System.Collections.Generic;
using System.Linq; 

namespace RestApiProject.Entitis
{
    public class Restaurant
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public bool hasDelivery { get; set; }

        public string ContactEmail { get; set; }

        public string ContactNumber { get; set; }

        
        public int AdressId { get; set; }

        public virtual Address Address { get; set; }
        
        public virtual List<Dish> Dishes { get; set; } 


    }
}
