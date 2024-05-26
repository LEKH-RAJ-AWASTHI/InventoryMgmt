using InventoryMgmt.DataAccess;
using InventoryMgmt.Model;
using InventoryMgmt.Model.ApiUseModel;
using InventoryMgmt.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Data;
using InventoryMgmt.Model.StoredProcedureModel;
using System.Data.Common;
using StoredProcedureEFCore;
using Microsoft.OpenApi.Any;
using System.Dynamic;
using FluentValidation;
using FluentValidation.Results;
using InventoryMgmt.CustomException;
using Serilog;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventoryMgmt.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IItemService _itemService;
        private readonly IReusableLogic _logic;
        private readonly IValidator<AddItemFormModel> _addItemValidator;
        private readonly IValidator<ItemFormModel> _updateItemValidator;

        public ItemController
            (
                IReusableLogic logic,
                IItemService itemService,
                ApplicationDbContext context,
                IValidator<AddItemFormModel> validator,
                IValidator<ItemFormModel> updateItemValidator
            )
        {
            _logic = logic;
            _context = context;
            _itemService = itemService;
            _addItemValidator = validator;
            _updateItemValidator = updateItemValidator;
        }

        //_itemService= _serviceLocator.GetService(ask for IItemService object)

        // GET: api/<ItemController>
        /// <summary>
        /// this endpoint
        /// </summary>
        /// <returns></returns>
        /// 
        [AllowAnonymous]
        [HttpGet("GetAllProduct")]
        public IActionResult Get()
        {
            try
            {
                List<GetItemModelDTO> items = new List<GetItemModelDTO>();
                items = (List<GetItemModelDTO>)_itemService.GetAll();
                if (items is null)
                {
                    Log.Error("Item Cannot be found in database");
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
        [HttpGet("GetItemById")]
        public IActionResult Get(int id)
        {
            try
            {

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
        public async Task<IActionResult> Post([FromBody] AddItemFormModel item)
        {
            try
            {
                if (item is null)
                {
                    Log.Error("Item is null here in AddNewItem API End Point");
                    throw new ArgumentNullException($"{nameof(item)} is required");
                }
                ValidationResult result = await _addItemValidator.ValidateAsync(item);
                string errorMessage = result.ToString("\n");
                if(errorMessage is not "")
                {
                    throw new InvalidOperationException(errorMessage);
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
                return StatusCode((int)UserDefinedErrorCode.ErrorCode.InsertItemError, ex.Message);
            }
        }
        //[AllowAnonymous]
        //[HttpGet("GetItemByStore")]
        //public IActionResult GetItemByStore(string storeName)
        //{
        //    try
        //    {
        //        var parameters = new Dictionary<string, object>
        //        {
        //            { "Store", storeName }
        //        };
        //        string procedureName = "FetchStockByStore";

        //        List<dynamic> result =ReusableLogic.ExecuteStoredProcedure(procedureName, parameters);
        //        return Ok(result);

        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        //    }
        //}

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
                    throw new FieldEmptyException("ItemId cannot be zero");
                }
                ValidationResult result = _updateItemValidator.Validate(item);
                string errorMessage = result.ToString("\n");
                if(errorMessage != null)
                {
                    throw new InvalidOperationException(errorMessage);
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
        [HttpPut("ChangeItemActiveStatus")]
        public IActionResult ChangeItemActiveStatus(int ItemId)
        {
            try
            {
                if(ItemId is 0)
                {
                    throw new Exception("Item Id cannot be zero");
                }
                bool response= _itemService.ChangeItemActiveStatus(ItemId);
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
        //[AllowAnonymous]
        [HttpGet("GetItemByStoreName")]
        public IActionResult GetResult(string storeName)
        {
            try
            {
                if (storeName is null && storeName is "")
                {
                    throw new InvalidOperationException("Item cannot be null and empty");
                }
                string procedureName = "FetchStockByStore";
                var parameters = new Dictionary<string, object>
                {
                    { "Store", storeName }
                };
                List<dynamic> result = _logic.ExecuteStoredProcedure(procedureName, parameters);

                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
        //[AllowAnonymous]
        [HttpPost("InsertBulkItem")]
        public IActionResult InsertBulkItem(int storeId, List<ItemFormModel> items)
        {

            int n = 0;
            try
            {
                if(items is null )
                {
                    throw new InvalidOperationException("Cannot insert null items");
                }
                foreach(ItemFormModel item in items)
                { 
                    if (item is null)
                    {
                        throw new ArgumentNullException($"{nameof(item)} is required");
                    }
                    ValidationResult result = _updateItemValidator.Validate(item);
                    string errorMessage = result.ToString("\n");
                    if (errorMessage is not "")
                    {
                        throw new InvalidOperationException(errorMessage);
                    }
                    n++;


                }
                bool response = _itemService.InsertBulkItems(storeId,items);
                if(!response)
                {
                    throw new InvalidOperationException("Cannot add Item");
                }
                return Ok($"{n} items added succesfully");
            }
            catch(Exception ex ) 
            {
                return StatusCode(StatusCodes.Status406NotAcceptable, ex.Message);
            }
        }
    }
}
