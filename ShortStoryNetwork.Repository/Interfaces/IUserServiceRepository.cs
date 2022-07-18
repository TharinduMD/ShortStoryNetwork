using ShortStoryNetwork.Core;

namespace ShortStoryNetwork.Repository.Interfaces
{
    public interface IUserServiceRepository
    {
        public UserLogin GetUser(UserLogin userInfo);
    }
}
