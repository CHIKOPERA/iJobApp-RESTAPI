using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace iJobRestApi.Controllers
{
    public class CheckAssignedController : ApiController
    {
        private static string your_username = "ijob", your_password = "Tanya@dodger12";
        private SqlConnection con = new SqlConnection($"Server=tcp:ijobsrvr.database.windows.net,1433;Initial Catalog=iJobDatabase;Persist Security Info=False;User ID={your_username};Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=50;");

        public JobRequests GetAssignment(string ClientID)
        {
            try
            {
                con.Open();
                if (con.State==System.Data.ConnectionState.Open)
                {
                    
                    char[] charsToRemove = { '{', '}' };
                    string theID = ClientID.Trim(charsToRemove);
                   // string Query = $"Select * From JobRequest where ClientID='{theID}'";
                    string Query = $"SELECT T1.* FROM JobRequest T1 WHERE T1.Date = (SELECT MAX(Date) FROM JobRequest WHERE ClientID='{theID}');";

                    SqlCommand cmd = new SqlCommand(Query, con);
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        if (dataReader.Read())
                        {
                            JobRequests jobRequest = new JobRequests();
                            jobRequest.ClientID = dataReader["ClientID"].ToString();
                            jobRequest.Date = dataReader["Date"].ToString();
                            jobRequest.TimeStart = dataReader["Time_Start"].ToString();
                            jobRequest.TimeEnd = dataReader["Time_End"].ToString();
                            jobRequest.Category = dataReader["Category"].ToString();
                            jobRequest.NumOfLabs = int.Parse(dataReader["Num_Of_Labourers"].ToString());
                            jobRequest.JobSpec = dataReader["Job_Specification"].ToString();
                            jobRequest.Location = dataReader["Location"].ToString();
                            jobRequest.TranspotStatus = Convert.ToBoolean(dataReader["Transport_Status"].ToString());
                            jobRequest.IsAssigned = Convert.ToBoolean(dataReader["Assigned_Status"].ToString());
                            jobRequest.Cost = Convert.ToDecimal(dataReader["Cost"].ToString());
                            con.Close();
                            return jobRequest;

                        }
                        con.Close();
                        return null;

                    }
                    else
                    {
                        con.Close();
                        return null;

                    }
                }
                else
                {
                    con.Close();
                    return null;
                }
            }
            catch (Exception)
            {

                con.Close();
                return null;

            }
        }
    }
}
