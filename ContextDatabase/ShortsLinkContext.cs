using System;
using ContextDatabase.Models;
using Microsoft.EntityFrameworkCore;

namespace ContextDatabase
{
    public partial class ShortsLinkContext : DbContext
    {
        public DbSet<ShortsTbl> ShortsTbl { get; set; }

        public ShortsLinkContext(DbContextOptions<ShortsLinkContext> options) : base(options)
        {
        }
    }
}