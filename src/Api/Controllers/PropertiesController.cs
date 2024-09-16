using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{

    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class PropertiesController(PropertyService propertyService) : ControllerBase
    {

        private readonly PropertyService _propertyService = propertyService;

        /* Controller New property */
        [HttpPost]
        public async Task<IActionResult> CreateProperty([FromBody] Property property)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _propertyService.CreatePropertyAsync(property);
            return Ok(property);
        }
        /* Controller New property */

        /* Add Img */
        [HttpPost("{id}/add-image")]
        public async Task<IActionResult> AddImage(int id, [FromBody] PropertyImage image)
        {

            if (image == null)
                return BadRequest("Image data is required.");

            await _propertyService.AddImageToPropertyAsync(id, image);
            return Ok();
        }
        /* Add Img */

        /* Change price */
        [HttpPut("{id}/change-price")]
        public async Task<IActionResult> ChangePrice(int id, [FromBody] decimal newPrice)
        {
            await _propertyService.ChangePropertyPriceAsync(id, newPrice);
            return Ok();
        }
        /* Change price */

        /* Update property */
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProperty(int id, [FromBody] Property property)
        {
            if (id != property.IdProperty)
                return BadRequest("ID mismatch");

            await _propertyService.UpdatePropertyAsync(property);
            return Ok();
        }
        /* Update property */

        /* List All */
        [HttpGet]
        public async Task<IActionResult> ListProperties([FromQuery] string filterName, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var properties = await _propertyService.ListPropertiesAsync(filterName, minPrice, maxPrice, page, pageSize);
            return Ok(properties);
        }
        /* List All */

    }

}