using System;
using System.Collections.Generic;
using System.Linq;

namespace RestApiProject.Models
{
    // Class DTO is a model of an existing class but doesn't contain all the data that is present in the database table
    //The model is used to comunicate directely with the client and it shows only thoes columns from the table
    //that the programmer wants to be displayed

    public class RestaurantDto
    {
        // Here only those properties will be seen by client

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public bool hasDelivery { get; set; }

        // properties containing the address 

        public string City { get; set; }

        public string Street { get; set; }

        public string PostalCode { get; set; }  

        // info about dishes

        public List<DishDto> DishesDto { get; set; }
    }
}
