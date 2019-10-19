using iJobRestApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace iJobRestApi.Controllers
{
    public class PlacementController : ApiController
    {
        private static string your_username = "ijob", your_password = "Tanya@dodger12";
        private SqlConnection con = new SqlConnection($"Server=tcp:ijobsrvr.database.windows.net,1433;Initial Catalog=iJobDatabase;Persist Security Info=False;User ID={your_username};Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=50;");


        iJobRestApi.Models.JobPlacementEntities db = new JobPlacementEntities();
        [System.Web.Http.HttpPost]
        [System.Web.Http.ActionName("PlaceJobRequest")]
        public HttpResponseMessage MakePlacement(int jobRequestID,string LabID, string AgentID,string DriverID,string Date,bool Completion)
        {
            try
            {
               
                iJobRestApi.Models.Placement placement = new Placement();

                placement.Agent_ID = AgentID;
                placement.Completion_Status = Completion;
                placement.Date = Convert.ToDateTime( Date);
                placement.Job_Request_ID = jobRequestID;
                placement.Labourer_ID = LabID;
                placement.Driver_ID = DriverID;
                db.Placements.Add(placement);                           
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.Accepted, "Your Placement has been Made");
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }


    
        


    }
}
