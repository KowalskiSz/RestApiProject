using System.Collections.Generic;
using System.Linq;

using AutoMapper;
using RestApiProject.Entitis;
using RestApiProject.Models;

namespace RestApiProject
{
    // Class that mapps default created class to dto type that contains only certain info from BD for 
    // user / cilent
    public class RestaurantMappingProfile: Profile
    {
        // All the mapping stuff is being done in the constructor

        public RestaurantMappingProfile()
        {
            //Mapping refers only thoes properties that do not match names (if they match - mapping is done automaticly)
            CreateMap<Restaurant, RestaurantDto>().ForMember(dto => dto.City, m => m.MapFrom(c => c.Address.City)).
                ForMember(dto => dto.Street, m => m.MapFrom(s => s.Address.Street)).
                ForMember(dto => dto.PostalCode, m => m.MapFrom(p => p.Address.PostalCode));

            // Mapping to DishDto
            CreateMap<Dish, DishDto>();

            //Mapping to CreateRestaurantDto
            CreateMap<CreateRestaurantDto, Restaurant>().
                ForMember(r => r.Address, d => d.MapFrom(obj => new Address
                {
                    City = obj.City,
                    Street = obj.Street,
                    PostalCode = obj.PostalCode
                }));



        }


    }
}
