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
    }
}
