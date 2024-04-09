using InventoryMgmt.Model;
using InventoryMgmt.Model.ApiUseModel;
using InventoryMgmt.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventoryMgmt.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        // GET: api/<ItemController>
        [HttpGet("GetAllProduct")]
        public IActionResult Get()
        {
            try
            {
                List<ItemModel> items = new List<ItemModel>();
                items = (List<ItemModel>)_itemService.GetAll();
                if(items is null)
                {
                    throw new Exception("Items not found");
                }
            
                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, $"Exception: {ex}");
            }
        }

        // GET api/<ItemController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {

                if(id is 0)
                {
                    throw new Exception("ItemId cannot be zero");
                }
                ItemModelClass itemModelClass = new ItemModelClass();
                itemModelClass = _itemService.Get(id);
                return Ok(itemModelClass);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, $"Exception: {ex}");

            }
        }

        // POST api/<ItemController>
        [HttpPost("AddNewItem")]
        public IActionResult Post([FromBody] AddItemFormModel item)
        {
            try
            {
                if (item is null)
                {
                    throw new ArgumentNullException($"{nameof(item)} is required");
                }
                bool response = _itemService.AddItem(item);
                if (response)
                {
                    return Ok("Item added successfully");
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        // PUT api/<ItemController>/5
        [HttpPut("UpdateItem")]
        public IActionResult Put(int itemId, ItemFormModel item)
        {
            try
            {
                if(item is null)
                {
                    throw new ArgumentNullException($"{nameof(item)} is required");
                }
                if (itemId is 0)
                {
                    throw new Exception("ItemId cannot be zero");
                }
                bool response = _itemService.Update(itemId, item);
                if (response)
                {
                    return Ok("Item updated Successfully");
                }
                else 
                { 
                    return BadRequest("Unknown error"); 
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        // DELETE api/<ItemController>/5
        [HttpDelete("Delete Item")]
        public IActionResult Delete(int ItemId)
        {
            try
            {
                if(ItemId is 0)
                {
                    throw new Exception("Item Id cannot be zero");
                }
                bool response= _itemService.Delete(ItemId);
                if (response)
                {
                    return Ok("Item deleted successfully");
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);

            }
        }
    }
}
