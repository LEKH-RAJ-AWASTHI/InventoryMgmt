using InventoryMgmt.DataAccess;
using InventoryMgmt.Model;
using InventoryMgmt.Model.ApiUseModel;
using InventoryMgmt.Model.DTOs;
using InventoryMgmt.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Serilog;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventoryMgmt.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }
        // GET: api/<StoreController>
        [HttpGet("GetAllStore")]
        public IActionResult Get()
        {
            if (_storeService.ShowAllStores == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "User Not Found in the Server");
            }
            List<StoreRegisterModel> list = new List<StoreRegisterModel>();
            list = _storeService.ShowAllStores();
            return Ok(list);
        }
        // POST api/<StoreController>
        [HttpPost("AddStore")]
        public IActionResult Post([FromBody] AddStoreDTO addStoreDTO)
        {
            string StoreName = addStoreDTO.StoreName;
            try
            {

                if (StoreName is null)
                {
                    throw new ArgumentNullException($"{nameof(StoreName)} is required!");
                }
                bool response = _storeService.AddStore(StoreName);
                if (response)
                {
                    return Ok("Store Added Successfully");
                }
                else
                {
                    return BadRequest("Something Went Wrong");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, $"Exception: {ex}");
            }

        }

        // PUT api/<StoreController>/5
        [HttpPut("UpdateStore")]
        public IActionResult Put(UpdateStoreDTO updateStoreDTO)
        {
            string oldStoreName = updateStoreDTO.OldStoreName;
            string newStoreName = updateStoreDTO.NewStoreName;

            try
            {
                if (oldStoreName is null && oldStoreName is "")
                {
                    throw new ArgumentNullException($"{nameof(oldStoreName)} is required!");
                }
                bool response = _storeService.UpdateStore(oldStoreName, newStoreName);
                if (response)
                {
                    return Ok("Store Updated Successfully");
                }
                else
                {
                    return BadRequest("Something Went Wrong");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, $"Exception: {ex}");

            }

        }

        // DELETE api/<StoreController>/5
        [HttpPut("ChangeStoreActiveStatus")]
        public IActionResult ChangeStoreActiveStatus(AddStoreDTO addStoreDTO)
        {
            string StoreName = addStoreDTO.StoreName;
            try
            {
                if (StoreName == null)
                {
                    throw new ArgumentNullException($"{nameof(StoreName)} is required!");
                }
                bool response = _storeService.ChangeStoreActiveStatus(StoreName);
                if (response)
                {
                    return Ok("Store Deleted Successfully");
                }
                else
                {
                    return BadRequest("Something Went Wrong");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, $"Exception {ex}");
            }
        }
        [AllowAnonymous]
        [HttpGet("StockLevel")]
        public IActionResult GetStockLevel()
        {
            try
            {
                List<dynamic> list = _storeService.showStockLevel();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex);
            }
        }
        //[AllowAnonymous]
        //[HttpPut("SalesOfItem")]
        //public IActionResult SalesOfItem(int  StoreId, int ItemId, decimal Quantity)
        //{
        //    if(StoreId is 0  && ItemId is 0 && Quantity is 0)
        //    {
        //        Log.Error("User Input is zero in SalesOfItem API Endpoint");
        //        return StatusCode(StatusCodes.Status400BadRequest, "Given Fields Cannot be Zero");
        //    }
        //    else
        //    {
        //        bool response =_storeService.SalesOfItem(StoreId, ItemId, Quantity);
        //        if (response) { return Ok("Sale of item successful"); }
        //        else
        //        {
        //            return BadRequest();
        //        }
        //    }
        //}



    }
}
