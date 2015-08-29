using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;

namespace TestRedis.DAL
{
    public static class EFContextHelper
    {
        public static string ConnectionString(string connectionName, string databaseName)
        {
            return ConnectionString(connectionName, databaseName, "", "", "");
        }
        public static string ConnectionString(string connectionName, string databaseName, string modelName, string userName, string password)
        {
            bool integratedSecurity = (userName == "");

            if (modelName == "")
            {
                return new SqlConnectionStringBuilder
                {
                    MultipleActiveResultSets = true,
                    InitialCatalog = databaseName,
                    DataSource = connectionName,
                    IntegratedSecurity = integratedSecurity,
                    UserID = userName,
                    Password = password,
                }.ConnectionString;
            }
            else
            {
                return new EntityConnectionStringBuilder
                {
                    Metadata = "res://*/" + modelName + ".csdl|res://*/" + modelName + ".ssdl|res://*/" + modelName + ".msl",
                    Provider = "System.Data.SqlClient",
                    ProviderConnectionString = new SqlConnectionStringBuilder
                    {
                        MultipleActiveResultSets = true,
                        InitialCatalog = databaseName,
                        DataSource = connectionName,
                        IntegratedSecurity = integratedSecurity,
                        UserID = userName,
                        Password = password
                    }.ConnectionString
                }.ConnectionString;
            }
        }
    }
}
