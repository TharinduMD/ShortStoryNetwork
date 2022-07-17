using ShortStoryNetwork.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShortStoryNetwork.Repository.Interfaces
{
    public interface IPostRepository
    {
        string Message { get; set; }
        string Result { get; set; }

        bool AddPost(Post post);

    }
}
