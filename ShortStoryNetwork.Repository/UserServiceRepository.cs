using ShortStoryNetwork.Core;
using ShortStoryNetwork.Data;
using ShortStoryNetwork.Repository.Interfaces;
using System.Linq;

namespace ShortStoryNetwork.Repository
{
    public class UserServiceRepository : IUserServiceRepository
    {
        private readonly Context _context;


        public UserServiceRepository(Context context)
        {
            _context = context;
        }
        public UserLogin GetUser(UserLogin userInfo)
        {
            var userLogin = new UserLogin();
            var user = (from userQ in _context.UserInfos
                        where
                            userQ.EmailAddress == userInfo.Email &&
                            userQ.PasswordHash == userInfo.Password
                        select userQ).FirstOrDefault();

            if (user != null)
            {
                userLogin = new UserLogin
                {
                    UserName = user.FirstName,
                    Email = user.EmailAddress,
                    UserId = user.UserId
                };
            }
            return userLogin;
        }
    }
}
