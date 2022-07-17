using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Logging;
using ShortStoryNetwork.Core;
using ShortStoryNetwork.Data;
using ShortStoryNetwork.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ShortStoryNetwork.Repository
{
    public class UserInfosRepository : IUserInfosRepository
    {
        private readonly ILogger<UserInfosRepository> _logger;
        private readonly Context _context;

        public string Message { get; set; }
        public string Result { get; set; }

        public UserInfosRepository(ILogger<UserInfosRepository> logger, Context context)
        {
            _logger = logger;
            _context = context;
        }
        public bool AddUser(UserInfo userInfo)
        {
            var success = false;
            try
            {
                var userExit = _context.UserInfos.Find(userInfo.UserId);
                if (userExit == null)
                {
                    byte[] salt = new byte[128 / 8];
                    using (var rngCsp = new RNGCryptoServiceProvider())
                    {
                        rngCsp.GetNonZeroBytes(salt);
                    }

                    // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
                    string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: userInfo.PasswordHash,
                        salt: salt,
                        prf: KeyDerivationPrf.HMACSHA256,
                        iterationCount: 100000,
                        numBytesRequested: 256 / 8));

                    userInfo.PasswordHash = hashed;
                    userInfo.IsEditor = string.IsNullOrEmpty(userInfo.IsEditor) ?
                                            userInfo.UserRole != "M" ? BooleanValue.False.ToString() : string.Empty : userInfo.IsEditor;
                    userInfo.IsBanned = string.IsNullOrEmpty(userInfo.IsBanned) ?
                                            userInfo.UserRole != "M" ? BooleanValue.False.ToString() : string.Empty : userInfo.IsBanned;

                    _context.Add(userInfo);
                    _context.SaveChanges();
                    success = true;
                    Result = userInfo.UserId;
                }
                else
                {
                    Message = "User already exists";
                }
                return success;
            }
            catch (Exception e)
            {
                _logger.LogError(e.StackTrace);
                Message = "Error occured when saving user";
                throw;
            }
        }
    }
}
