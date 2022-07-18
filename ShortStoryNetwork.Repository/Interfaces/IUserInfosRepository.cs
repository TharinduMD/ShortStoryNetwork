using ShortStoryNetwork.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShortStoryNetwork.Repository.Interfaces
{
    public interface IUserInfosRepository
    {
        string Message { get; set; }
        string Result { get; set; }
        bool AddUser(UserInfo userInfo);
        List<UserInfo> GetWriters();
    }
}
