using Microsoft.EntityFrameworkCore;
using ShortStoryNetwork.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShortStoryNetwork.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
                
        }

        public virtual DbSet<UserInfo> UserInfos { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<StatVowel> StatVowels { get; set; }
    }
}
