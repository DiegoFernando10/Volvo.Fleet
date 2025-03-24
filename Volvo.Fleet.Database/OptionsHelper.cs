using Microsoft.EntityFrameworkCore;

namespace Volvo.Fleet.Database
{
    public static class OptionsHelper
    {
        public static DbContextOptionsBuilder UseMySql(this DbContextOptionsBuilder options)
        {
            var host = "db-diego-fernando.cep2cma44q6i.us-east-1.rds.amazonaws.com";
            var username = "sa";
            var pass = "admin1234";

            var connString = $"Server={host};Database=dbo;Uid={username};Pwd='{pass}';CharSet=utf8;Connection Timeout=30";

            return options.UseMySql(connString, ServerVersion.AutoDetect(connString));
        }
    }
}