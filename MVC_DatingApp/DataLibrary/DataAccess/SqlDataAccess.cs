using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;
using Dapper;

namespace DataLibrary.DataAccess {
    public static class SqlDataAccess {

        public static string GetConnectionString(string connectionName = "DefaultConnection") {

            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;

        }


        public static List<T> LoadData<T>(string sql) {

            using (IDbConnection cnn = new SqlConnection(GetConnectionString())) {

                return cnn.Query<T>(sql).ToList();
            }
        }

        public static int SaveData<T>(string sql, T Data) {

            using (IDbConnection cnn = new SqlConnection(GetConnectionString())) {

                return cnn.Execute(sql, Data);
            }
        }
    }
}
