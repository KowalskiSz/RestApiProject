using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace RestApiProject.Entitis
{
    public class UpdateRestaurantDto
    {
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        public string Description { get; set; }

        public bool hasDelivery { get; set; }

    }
}
