using Microsoft.Extensions.Configuration;
using System.Text;

namespace Common
{
    public static class ConfigurationManagerExtensions
    {
        public static string GetConnectionStringWithLoginCredentials(this ConfigurationManager configurationManger, string localDbConnectionString, string containerizedDbConnectionString)
        {
            string connectionStingName = containerizedDbConnectionString;
            if (configurationManger["RUN_PROFILE"] == "Local")
                connectionStingName = localDbConnectionString;

            var rawConnectionString = new StringBuilder(configurationManger.GetConnectionString(connectionStingName));
            var connectionString = rawConnectionString
                .Replace("ENVID", configurationManger["DB_UID"])
                .Replace("ENVDBPW", configurationManger["DB_PW"])
                .ToString();

            return connectionString;
        }
    }
}
