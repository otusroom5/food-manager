using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodUserNotifier.Infrastructure.Repositories.Context
{
    internal class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            dbContextOptionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=FoodStorage;Username=postgres;Password=postgre");
            return new DatabaseContext(dbContextOptionsBuilder.Options);
        }
    }
}
