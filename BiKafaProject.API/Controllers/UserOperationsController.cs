using BiKafaProject.Core.Filters;
using BiKafaProject.Core.Interfaces;
using BiKafaProject.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiKafaProject.API.Controllers
{

    [Route("api/[controller]")]
    public class UserOperationsController : Controller
    {

        private readonly IUserOperationsRepository _userOperationsRepository;

        public UserOperationsController(IUserOperationsRepository userOperationsRepository)
        {
            _userOperationsRepository = userOperationsRepository;
        }

        
        public Task<IActionResult> GetData(string id)
        {
            if (id == null)
            { return this.GetPersonInfo(); }
            else
            {
                return GetPersonInfoById(id);
            }
           
        }

        private async Task<IActionResult> GetPersonInfo()
        {
            var data = await _userOperationsRepository.GetAsync(null);
            return Ok(data);
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonInfoById(string id)
        {
            var data = await _userOperationsRepository.GetAsync(id);
            return Ok(data);
        }

        [ValidationFilter]
        [HttpPost]
        public async Task<IActionResult> SaveData([FromBody] UserModel usermodel)
        {
            await _userOperationsRepository.SaveAsync(usermodel);
            return Created(String.Empty, usermodel);
        }


        [ValidationFilter]
        [HttpPut]
        public async Task<IActionResult> UpdateData([FromBody] UserModel usermodel)
        {
            await _userOperationsRepository.UpdateAsync(usermodel);
            return Accepted(usermodel);
        }

        [HttpDelete]
        public Task<IActionResult> DeleteData(string id)
        {
            if (id == null)
                return this.RemoveData();
            return RemoveDataById(id);
        }

        private async Task<IActionResult> RemoveData()
        {
            await _userOperationsRepository.DeleteAsync(null);
            return Ok();
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveDataById(string id)
        {
            await _userOperationsRepository.DeleteAsync(id);
            return Ok();
        }


    }
}
