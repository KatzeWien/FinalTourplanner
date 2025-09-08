using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTourplanner.DL
{
    public class GetDBContext
    {
        public static MyDBContext GetConnection(string DbString)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();
            var connection = config.GetConnectionString(DbString);
            var options = new DbContextOptionsBuilder<MyDBContext>()
                .UseNpgsql(connection)
                .Options;
            return new MyDBContext(options);
        }
    }
}
