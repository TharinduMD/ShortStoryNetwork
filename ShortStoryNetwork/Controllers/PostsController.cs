using Microsoft.AspNetCore.Mvc;
using ShortStoryNetwork.Core;
using ShortStoryNetwork.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShortStoryNetwork.Controllers
{
    [Route("post")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        public PostsController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        [HttpPost]
        public IActionResult Post(Post post)
        {
            var success = _postRepository.AddPost(post);
            try
            {
                if (success)
                {
                    var response = new ResponseObject
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Data = _postRepository.Result
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
                    Data = _postRepository.Result,
                    Error = new Error { Message = _postRepository.Message }
                };
                return BadRequest(response);
            }
        }

        //// PUT api/<PostsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<PostsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
        //// GET: api/<PostsController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<PostsController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

    }
}
