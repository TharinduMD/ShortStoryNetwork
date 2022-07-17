using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShortStoryNetwork.Core;
using ShortStoryNetwork.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShortStoryNetwork.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private readonly IUserInfosRepository _userInfosRepository;

        public UserInfoController(IUserInfosRepository userInfosRepository)
        {
            _userInfosRepository = userInfosRepository;
        }

        //// GET all
        //[HttpGet]
        //public IEnumerable<UserInfo> Get()
        //{
        //    return;
        //}

        //// GET by Id
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<UserInfoController>
        [HttpPost]
        public IActionResult Post(UserInfo userInfo)
        {
            var success = _userInfosRepository.AddUser(userInfo);
            try
            {
                if (success)
                {
                    var response = new ResponseObject
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Data = _userInfosRepository.Result
                    };
                    return Ok(response);
                }
                return NoContent();
            }
            catch (Exception)
            {
                var response = new ResponseObject
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Data = _userInfosRepository.Result,
                    Error = new Error { Message = _userInfosRepository.Message}
                };
                return BadRequest(response);
            }
        }

        //// PUT api/<UserInfoController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<UserInfoController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
