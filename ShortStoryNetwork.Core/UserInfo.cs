using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ShortStoryNetwork.Core
{
    [Table("UserInfo")]
    public class UserInfo
    {
        [Key]
        public string UserId { get; set; }
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        public string UserRole { get; set; }
        public string IsEditor { get; set; }
        public string IsBanned { get; set; }

        public UserInfo()
        {
            PasswordHash = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            EmailAddress = string.Empty;
            UserRole = UserRoleEnum.U.ToString();
            IsEditor = BooleanValue.False.ToString();
            IsBanned = BooleanValue.False.ToString();
        }
    }

    public enum UserRoleEnum
    {
        U = 1,
        M = 2,
        W = 3,
        E = 4
    }
    public enum BooleanValue
    {
        False,
        True
    }
}
