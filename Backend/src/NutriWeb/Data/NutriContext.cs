using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NutriWeb.Models;

namespace NutriWeb.Data
{
    public class NutriContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public NutriContext(DbContextOptions<NutriContext>options) : base(options)
        {
            
        }
    }
}