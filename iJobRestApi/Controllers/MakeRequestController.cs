using iJobRestApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace iJobRestApi.Controllers
{
    public class MakeRequestController : ApiController
    {
      private  static string your_username = "ijob", your_password = "Tanya@dodger12";

        iJobDatabaseConStrng db = new iJobDatabaseConStrng();
        [System.Web.Http.HttpPost]
        [System.Web.Http.ActionName("Add Request")]
        public HttpResponseMessage Add_Request(string ID, DateTime date,string timeStart,string timeEnd,bool transport,string category,int numOfLab,string location,decimal cost,string jobSpec)
        {
            try
            {
                JobRequest jobRequest = new JobRequest();
                jobRequest.ClientID = ID;
                jobRequest.Date = date;
                jobRequest.Time_Start = TimeSpan.Parse(timeStart);
                jobRequest.Time_End = TimeSpan.Parse(timeEnd); ;
                jobRequest.Transport_Status = transport;
                jobRequest.Assigned_Status = false;
                jobRequest.Category = category;
                jobRequest.Num_Of_Labourers = numOfLab;
                jobRequest.Job_Specification = jobSpec;
                jobRequest.Location = location;
                jobRequest.Cost = cost;
                
                db.JobRequests.Add(jobRequest);
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.Accepted, "Your request has been sent");
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }



        [System.Web.Http.HttpGet]
        [System.Web.Http.ActionName("CheckAssigned")]
        public HttpResponseMessage CheckAssigned(string ClientID)
        {
            SqlConnection con = new SqlConnection($"Server=tcp:ijobsrvr.database.windows.net,1433;Initial Catalog=iJobDatabase;Persist Security Info=False;User ID={your_username};Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=40;");

            SqlCommand sqlCommand = new SqlCommand($"SELECT TOP 1 * FROM JobRequest WHERE CONVERT(VARCHAR ,ClientID)='{ClientID}'",con);
            try
            {                      
                con.Open();
                if (con.State==System.Data.ConnectionState.Open)
                {
                    if ((int)sqlCommand.ExecuteScalar()>1)
                    {
                        if (true)
                        {
                            bool isAssigned = false;
                            SqlDataReader dataReader;
                            dataReader = sqlCommand.ExecuteReader();
                            if (dataReader.Read())
                            {
                                isAssigned = Convert.ToBoolean(dataReader["Assigned_Status"].ToString());
                                if (isAssigned == true)
                                {
                                    con.Close();
                                    return Request.CreateResponse(HttpStatusCode.Accepted, "Job Assigned");
                                }
                                else
                                {
                                    con.Close();
                                    return Request.CreateResponse(HttpStatusCode.OK, "Job Not assigned");
                                }
                            }
                            else
                            {
                                con.Close();
                                return Request.CreateResponse(HttpStatusCode.NotFound, "Data reader error");
                            }
                        }
                    }
                    else
                    {
                        con.Close();
                        return Request.CreateResponse(HttpStatusCode.Forbidden, "No record found");
                    }
                  
                }
                else
                {
                    con.Close();
                    return Request.CreateResponse(HttpStatusCode.Forbidden,"Connection not open");
                }
            }
            catch (Exception ex)
            {
                con.Close();
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);

            }

        }

    }
}
