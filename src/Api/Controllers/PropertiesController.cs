using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Application.Services;

namespace Api.Controllers
{
    /// <summary>
    /// API controller to manage properties, including creating, updating, and listing properties,
    /// as well as adding images and changing prices.
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class PropertiesController(PropertyService propertyService) : ControllerBase
    {

        private readonly PropertyService _propertyService = propertyService;

        /// <summary>
        /// Creates a new property in the system.
        /// </summary>
        /// <param name="property">The property object to be created.</param>
        /// <returns>An <see cref="IActionResult"/> with the created property details.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateProperty([FromBody] Property property)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _propertyService.CreatePropertyAsync(property);
            return Ok(property);
        }

        /// <summary>
        /// Adds an image to an existing property.
        /// </summary>
        /// <param name="id">The ID of the property to which the image will be added.</param>
        /// <param name="image">The image data to be added to the property.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.</returns>
        [HttpPost("{id}/add-image")]
        public async Task<IActionResult> AddImage(int id, [FromBody] PropertyImage image)
        {

            if (image == null)
                return BadRequest("Image data is required.");

            await _propertyService.AddImageToPropertyAsync(id, image);
            return Ok();
        }


        /// <summary>
        /// Changes the price of an existing property.
        /// </summary>
        /// <param name="id">The ID of the property whose price will be changed.</param>
        /// <param name="newPrice">The new price to be set for the property.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.</returns>
        [HttpPut("{id}/change-price")]
        public async Task<IActionResult> ChangePrice(int id, [FromBody] decimal newPrice)
        {
            await _propertyService.ChangePropertyPriceAsync(id, newPrice);
            return Ok();
        }

        /// <summary>
        /// Updates an existing property with new data.
        /// </summary>
        /// <param name="id">The ID of the property to be updated.</param>
        /// <param name="property">The updated property data.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the update operation.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProperty(int id, [FromBody] Property property)
        {
            if (id != property.IdProperty)
                return BadRequest("ID mismatch");

            await _propertyService.UpdatePropertyAsync(property);
            return Ok();
        }

        /// <summary>
        /// Retrieves a list of properties with optional filters and pagination.
        /// </summary>
        /// <param name="filterName">Optional filter by property name.</param>
        /// <param name="minPrice">Optional filter by minimum property price.</param>
        /// <param name="maxPrice">Optional filter by maximum property price.</param>
        /// <param name="page">The page number for pagination. Default is 1.</param>
        /// <param name="pageSize">The number of properties per page for pagination. Default is 10.</param>
        /// <returns>An <see cref="IActionResult"/> containing the list of filtered properties.</returns>
        [HttpGet]
        public async Task<IActionResult> ListProperties([FromQuery] string filterName, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var properties = await _propertyService.ListPropertiesAsync(filterName, minPrice, maxPrice, page, pageSize);
            return Ok(properties);
        }
    }

}