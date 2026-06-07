using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantMenu.Application.DTOs;
using RestaurantMenu.Application.Interfaces;

namespace RestaurantMenu.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemsController : ControllerBase
    {
        private readonly IMenuItemService _menuItemService;

        public MenuItemsController(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItemDto>>> GetAll()
        {
            var menuItems = await _menuItemService.GetAllAsync();

            return Ok(menuItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItemDto>> GetById(int id)
        {
            var menuItem = await _menuItemService.GetByIdAsync(id);
            if (menuItem == null)
            {
                return NotFound();
            }
            return Ok(menuItem);
        }

        [HttpPost]
        public async Task<ActionResult<MenuItemDto>> Create(CreateMenuItemDto dto)
        {
            try
            {
                var createdMenuItem = await _menuItemService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = createdMenuItem.Id }, createdMenuItem);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateMenuItemDto dto)
        {
            try
            {
                var updated = await _menuItemService.UpdateAsync(id, dto);
                if (!updated)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _menuItemService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();


        }



    }
}
