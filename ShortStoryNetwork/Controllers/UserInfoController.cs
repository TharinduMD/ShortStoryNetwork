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

        [HttpGet("writers")]
        public IActionResult Get()
        {
            var writers = _userInfosRepository.GetWriters();
            try
            {
                if (writers.Count > 0)
                {
                    var response = new ResponseObject
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Data = writers
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
                    Error = new Error { Message = _userInfosRepository.Message }
                };
                return BadRequest(response);
            }
        }

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
    }
}
