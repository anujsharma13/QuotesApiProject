using Microsoft.EntityFrameworkCore;
using QuotesApiProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuotesApiProject.Data
{
    public class QuotesDbContext:DbContext
    {
        public QuotesDbContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {

        }
       public DbSet<Quote> Quotes { get; set; }
    }
}
