using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

using RestApiProject.Entitis;
using RestApiProject.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NLog;
using Microsoft.Extensions.Logging;
using RestApiProject.Exceptions;

namespace RestApiProject.Services
{
    //Interface create to implement class RestauramtService as the service in Startup.cs file 
    //in roder to use it in the RestaurantController class
    public interface IRestaurantService
    {
        IEnumerable<RestaurantDto> GetAll();

        RestaurantDto GetById(int id);

        int CreateRes(CreateRestaurantDto createRestaurantDto);

        void DeleteRecord(int id);

        void UpdateRecord(UpdateRestaurantDto data, int id);
    }

    public class RestaurantService: IRestaurantService
    {
        // Consctructor (same as in restaurantSeeder) gets context of database
        private readonly RestaurantDbContext _dbContext;

        // Mapper to perfrom mapping in Controller class
        private readonly IMapper _mapper;

        //Logger property 
        private readonly ILogger<RestaurantService> _logger;

        //Injection in constructor of certain interfaces
        public RestaurantService(RestaurantDbContext dbContext, IMapper mapper, ILogger<RestaurantService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }


        public IEnumerable<RestaurantDto> GetAll()
        {
            //Getting raw data from database
            List<Restaurant> restaurans = _dbContext.Restaurants.Include(r => r.Address).
            Include(r => r.Dishes).
            ToList();

            var restaurantsDto = _mapper.Map<List<RestaurantDto>>(restaurans);
            return restaurantsDto;
        }

        
        public RestaurantDto GetById(int id)
        {
            var restaurant = _dbContext.Restaurants.
                Include(r => r.Address).
                Include(r => r.Dishes)
                .SingleOrDefault(r => r.Id == id);

            if (restaurant is null)
            {
                throw new NotFoundException("Restaurant not found");
            }

            //to Dto type
            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);

            return restaurantDto;

        }

        public int CreateRes(CreateRestaurantDto createRestaurantDto)
        {
            var restaurant = _mapper.Map<Restaurant>(createRestaurantDto);
            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();

            return restaurant.Id;
        }

        public void DeleteRecord(int id)
        {
            //logger Warning defined in this method (service)
            _logger.LogError($"restaurant with id:{id} DELETE invoked");
            
            var restaurant = _dbContext.Restaurants.FirstOrDefault(r => r.Id == id); 

            if(restaurant is null)
            {
                throw new NotFoundException("Restaurant not found");
            }

            _dbContext.Restaurants.Remove(restaurant);
            _dbContext.SaveChanges();
 
        }

        public void UpdateRecord(UpdateRestaurantDto data, int id)
        {
            var restaurant = _dbContext.Restaurants.FirstOrDefault(r => r.Id == id);
            if (restaurant is null)
            {
                throw new NotFoundException("Restaurant not found"); 
            }

            restaurant.Name = data.Name;
            restaurant.Description = data.Description;
            restaurant.hasDelivery = data.hasDelivery;

            _dbContext.SaveChanges();
        }

    }
}
