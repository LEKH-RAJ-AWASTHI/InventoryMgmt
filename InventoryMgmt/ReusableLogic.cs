using InventoryMgmt.DataAccess;
using InventoryMgmt.Model.StoredProcedureModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;
using System.Dynamic;
using System.Security.Cryptography.X509Certificates;
namespace InventoryMgmt
{
    public class ReusableLogic
    {
        
        public static ApplicationDbContext context = new ApplicationDbContext();
        public static List<dynamic> ExecuteStoredProcedure(string storedProcedureName, Dictionary<string, object> parameters)
        {
            //var result = new DataTable();
            //DbCommand cmd = context.Database.GetDbConnection().CreateCommand();
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "FetchStockByStore";
            //foreach (var parameter in parameters)
            //{
            //    cmd.Parameters.Add(new SqlParameter($"@{parameter.Key}", parameter.Value));
            //}
            //try
            //{
            //    // executes
            //    context.Database.OpenConnection();
            //    //var reader = cmd.ExecuteReader();
            //    using (var reader = cmd.ExecuteReader())
            //    {

            //        while (reader.Read())
            //        {
            //            result.Load(reader);
            //            //result.Load(reader);
            //        }
            //    }
            //}
            //finally
            //{
            //    // closes the connection
            //    ReusableLogic.context.Database.CloseConnection();
            //}
            //return result;
            var result = new List<dynamic>();

            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = storedProcedureName;
                command.CommandType = CommandType.StoredProcedure;
                //command.Parameters.Add(new SqlParameter("@Store", storeName));
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(new SqlParameter($"@{parameter.Key}", parameter.Value));
                }


                context.Database.OpenConnection();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var dataRow = new ExpandoObject() as IDictionary<string, object>;
                        for (var i = 0; i < reader.FieldCount; i++)
                        {
                            dataRow.Add(reader.GetName(i), reader[i]);
                        }
                        result.Add(dataRow);
                    }
                }
            }
            return result;
        }
    }
}
