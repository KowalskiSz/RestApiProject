using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

using RestApiProject.Entitis;
using RestApiProject.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestApiProject.Services;

namespace RestApiProject.Controllers
{

    // class defines actions on certians endpoints 
    [Route("api/restaurant")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        // To constructor inject: dbContext, mapper to map data
        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        // The GET section 
        // Method that returns all the restaurants in database
        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAllRestaurants()
        {

            var resultGetAll = _restaurantService.GetAll();


            return (Ok(resultGetAll));
        }

        //Method that returns certian restaurant from database by its id passed in URL 
        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> GetRestaurantById([FromRoute] int id)
        {

            var resultGetById = _restaurantService.GetById(id);

            if (resultGetById is null)
            {
                return NotFound();
            }


            return Ok(resultGetById);
        }

        // The POST section
        // Method that posts restaurant data in database (creating a new restaurant in db)
        [HttpPost]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto createRestaurantDto)
        {
            //Validation of required properties
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int resId = _restaurantService.CreateRes(createRestaurantDto);

            return Created($"api/restaurant/{resId}", null);
        }

        //The DELETE method
        //Function deletes record from database by its ID
        [HttpDelete("{id}")]
        public ActionResult DeleteRecord([FromRoute] int id)
        {
            bool result = _restaurantService.DeleteRecord(id);

            if (result)
            {
                return NoContent();
            }

            return NotFound();

        }

        //Changing entity by PUT  - change only Name, Description and HasDelivery 
        [HttpPut("{id}")]
        public ActionResult UpdateRecord([FromBody] UpdateRestaurantDto data, [FromRoute] int id)
        {
            //Validation
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool result = _restaurantService.UpdateRecord(data, id); 

            if(!result)
            {
                return NotFound();
            }

            return Ok();


        }

    }
}
