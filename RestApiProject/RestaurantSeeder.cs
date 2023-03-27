using RestApiProject.Entitis;
using System.Linq;
using System.Collections.Generic;

namespace RestApiProject
{
    public class RestaurantSeeder
    {
        // Database connection section (as always in construcotr)
        private RestaurantDbContext _dbContext; 

        public RestaurantSeeder(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        // Main method to seed DB with data
        public void Seed()
        {
            if(_dbContext.Database.CanConnect())
            {
                if(!_dbContext.Restaurants.Any())
                {
                    var createdData = GetData();
                    _dbContext.Restaurants.AddRange(createdData);
                    _dbContext.SaveChanges(); 
                }
            }
        }

        // Just for generating small dataset - in this case the restaurant table is being filled up along with two 
        // Address data and Dish data
        private IEnumerable<Restaurant> GetData()
        {
            // Creating list of Restaurant type of two restaurants
            var restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name = "KFC",
                    Category = "Fastfood",
                    Description = "KFC - an Amercian fastfood cain serving chicken",
                    hasDelivery = true,
                    ContactEmail = "kfc@kfc.com",
                    ContactNumber = "123456789",

                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Bismart",
                            Price = 5.50F
                        },

                        new Dish()
                        {
                            Name = "Chicken Nuggets",
                            Price = 10F
                        }
                    },

                    Address = new Address()
                    {
                        City = "Katowice",
                        Street = "Długa",
                        PostalCode = "44-526"
                    }


                },

                new Restaurant()
                {
                    Name = "McDonalds",
                    Category = "Fastfood",
                    Description = "McDonalds - an Amercian fastfood cain serving burggers mainly",
                    hasDelivery = true,
                    ContactEmail = "mcd@mcd.com",
                    ContactNumber = "554654654654",

                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Bigmac",
                            Price = 3.8F
                        },

                        new Dish()
                        {
                            Name = "McRoyal",
                            Price = 5.20F
                        }
                    },

                     Address = new Address()
                     {
                        City = "Tychy",
                        Street = "Krótka",
                        PostalCode = "43-100"
                     }
                }
            };


            return restaurants; 
        }

    }
}
