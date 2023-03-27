using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RestApiProject.Models
{
    // create restaurant dto
    public class CreateRestaurantDto
    {
        // Restaurant atributes

        //Validation on certain properties
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public bool hasDelivery { get; set; }

        public string ContactEmail { get; set; }

        public string ContactNumber { get; set; }

        //Address atributes

        [Required]
        [MaxLength(50)]
        public string City { get; set; }

        [Required]
        [MaxLength(50)]
        public string Street { get; set; }

        public string PostalCode { get; set; }


    }
}
