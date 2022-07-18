using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShortStoryNetwork.Core;
using ShortStoryNetwork.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ShortStoryNetwork.Controllers
{
    [Route("statvowel")]
    [ApiController]
    [Authorize(policy: "Moderators")]
    public class StatVowelController : ControllerBase
    {
        private readonly IStatVowelRepository _statVowelRepository;
        public StatVowelController(IStatVowelRepository statVowelRepository)
        {
            _statVowelRepository = statVowelRepository;
        }

        [HttpGet("staticperday")]
        public IActionResult Get(DateTime date)
        {            
            try
            {
                var statVowel = _statVowelRepository.GetStateVowelByDay(date);
                if (statVowel != null)
                {
                    var response = new ResponseObject
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Data = statVowel
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
                    Data = _statVowelRepository.Result,
                    Error = new Error { Message = _statVowelRepository.Message }
                };
                return BadRequest(response);
            }
        }

        [HttpGet("userdetails")]
        public IActionResult Get(string searchField = "", string searchText = "")
        {
            try
            {
                var users = _statVowelRepository.GetUserInfoList(searchField, searchText);
                if (users.Count > 0)
                {
                    var response = new ResponseObject
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Data = users
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
                    Data = _statVowelRepository.Result,
                    Error = new Error { Message = _statVowelRepository.Message }
                };
                return BadRequest(response);
            }
        }
    }
}
