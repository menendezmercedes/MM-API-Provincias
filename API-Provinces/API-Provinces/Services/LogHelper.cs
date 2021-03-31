using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace API_Provinces.Services
{
    public class LogHelper
    {
        public static void WriteFileLog(string message)
        {
           DirectoryInfo dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            var logPath = dir + "\\log.txt";
            if (!File.Exists(logPath))
            {
                File.Create(logPath).Close();
                
            }
            StreamWriter w = File.AppendText(logPath);
            
            w.WriteLine("Log Entry " + DateTime.Now.ToLongTimeString());
            w.WriteLine(message);
            w.Close();
           
                
        }

        public static void WriteDBLog(string message)
        {
            var connection = ConnectionHelper.setConnectionString();
            SqlConnection con = new SqlConnection(connection);
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_AuditLog_INS", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Message", SqlDbType.VarChar).Value = message;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("There was an issue adding audit log " + e.Message);
            }
        }

    }
}
