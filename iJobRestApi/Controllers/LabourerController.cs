using iJobRestApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace iJobRestApi.Controllers
{
    public class LabourerController : ApiController
    {
        iJobRestApi.Models.iJobLabConstring dbLabourer = new iJobLabConstring();
            [System.Web.Http.HttpPost]
            [System.Web.Http.ActionName("UpdateRating")]
        public HttpResponseMessage RateLabourer(string labID,int rating)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            } 
            iJobRestApi.Models.Labourer labourer = new Labourer();
             dbLabourer.Database.ExecuteSqlCommand($"Update Labourer Set Rating={rating} where Labourer_ID={labID}");       
           
            try
            {
                dbLabourer.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
            }
            return Request.CreateErrorResponse(HttpStatusCode.Accepted, "Record Updated");
        }
        //[System.Web.Http.HttpPost]
        //[System.Web.Http.ActionName("Update_Request")]
        //public HttpResponseMessage _Request()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
        //    }
        //    JobRequest jobRequest = new JobRequest();
        //    var entry = db.Entry<JobRequest>(jobRequest);
        //    entry.Entity.Cost = 333;
        //    entry.State = System.Data.Entity.EntityState.Modified;
        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException ex)
        //    {

        //        return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
        //    }
        //    return Request.CreateErrorResponse(HttpStatusCode.Accepted, "Record Updated");

        //} 
    }
}
