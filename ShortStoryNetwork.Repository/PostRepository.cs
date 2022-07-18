using Microsoft.Extensions.Logging;
using ShortStoryNetwork.Core;
using ShortStoryNetwork.Data;
using ShortStoryNetwork.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShortStoryNetwork.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ILogger<PostRepository> _logger;
        private readonly Context _context;
        private readonly IStatVowelRepository _statVowelRepository;
        public string Message { get; set; }
        public string Result { get; set; }

        public PostRepository(ILogger<PostRepository> logger, Context context, IStatVowelRepository statVowelRepository)
        {
            _logger = logger;
            _context = context;
            _statVowelRepository = statVowelRepository;
        }

        public bool AddPost(Post post)
        {
            var success = false;
            try
            {
                if(post != null)
                {

                    var postExists = _context.Posts.Where(x =>(x.Date).Date == DateTime.Today).ToList();
                    if(!postExists.Any()){
                        var insertToStateVowel = _statVowelRepository.AddStatVowel(post.Posts);
                    }
                    else{
                        var updateStateVowel = _statVowelRepository.UpdateStateVowel(post.Posts);
                    }
                    _context.Posts.Add(post);
                    _context.SaveChanges();
                    Result = post.PostId;
                    success = true;
                }
                return success;
            }
            catch(Exception e)
            {
                _logger.LogError(e.StackTrace);
                Message = "Error occured while saving";
                throw;
            }
        }
    }
}
