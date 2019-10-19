using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace iJobRestApi.Controllers
{
    public class ClientController : ApiController
    {
        private static string your_username = "ijob", your_password = "Tanya@dodger12";
        private SqlConnection con = new SqlConnection($"Server=tcp:ijobsrvr.database.windows.net,1433;Initial Catalog=iJobDatabase;Persist Security Info=False;User ID={your_username};Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=50;");
        iJobRestApi.Models.ClientConStrng db = new Models.ClientConStrng();
        [System.Web.Http.HttpPost]
        [System.Web.Http.ActionName("Add Client")]
        public HttpResponseMessage Add_Client(string ID, string Fname, string Lname, string date, string gender, string street, string city, string province, int postal_code, string email, string password)
        {
            try
            {
                iJobRestApi.Models.Client client = new Models.Client();
                client.ClientID = ID;
                client.Name = Fname;
                client.Surname = Lname;
                client.Date_Of_Birth = date;
                client.Gender = gender;
                client.Street_Address = street;
                client.City = city;
                client.Province = province;
                client.Postal_Code = postal_code;
                client.Email = email;
                client.Password = password;

                db.Clients.Add(client);
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.Accepted, "Your profile has been succesffuly  added");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);

            }
        }
        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////////
        /// </summary>
        iJobRestApi.Models.ClientConStrng db2 = new Models.ClientConStrng();
        //[System.Web.Http.HttpGet]
        //[System.Web.Http.ActionName("ifLogin")]
        //public HttpResponseMessage ifLogin(string Email,string password)
        //{
        //    try
        //    {
        //        con.Open();
        //        if (con.State == System.Data.ConnectionState.Open)
        //        {
        //            char[] charsToRemove = { '{', '}' };
        //            string email = Email.Trim(charsToRemove);

        //            string Query = $"SELECT * FROM Client where Email='{email}'";
        //            SqlCommand cmd = new SqlCommand(Query, con);



        //            string username = "", pass = "";
        //            SqlDataReader dataReader = cmd.ExecuteReader();
        //            if (dataReader.HasRows)
        //            {
        //                if (dataReader.Read())
        //                {
        //                    username = dataReader["Email"].ToString();
        //                    pass = dataReader["Password"].ToString();
        //                }
        //                if (pass == password.Trim(charsToRemove))
        //                {
        //                    con.Close();
        //                    return Request.CreateResponse(HttpStatusCode.Accepted, "Login Successful");

        //                }
        //                else
        //                {
        //                    con.Close();
        //                    return Request.CreateResponse(HttpStatusCode.NoContent, "The password is wrong");

        //                }

        //            }
        //            else
        //            {
        //                con.Close();
        //                return Request.CreateResponse(HttpStatusCode.NotFound, "The Client Profile does not exits");

        //            }
        //        }
        //        else
        //        {
        //            con.Close();
        //            return Request.CreateResponse(HttpStatusCode.OK, "Database not connected");

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        con.Close();
        //        return Request.CreateResponse(HttpStatusCode.ExpectationFailed, $"Error{ex.Message}");

        //    }
        //}

        [System.Web.Http.HttpGet]
        [System.Web.Http.ActionName("ifLogin")]
        public HttpResponseMessage ifLogin(string eemail, string password,ref string rCLientID, ref string rFirstn, ref string rSurname, ref string rDOB, ref string rGender, ref string rStreet, ref string rCity, ref string rProvince, ref string rZip, ref string rEmail, ref string rPassword)
        {
            try
            {
                con.Open();
                if (con.State == System.Data.ConnectionState.Open)
                {
                    char[] charsToRemove = { '{', '}' };
                    string email = eemail.Trim(charsToRemove);

                    string Query = $"SELECT * FROM Client where Email='{email}'";
                    SqlCommand cmd = new SqlCommand(Query, con);



                    string username = "", pass = "";
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        if (dataReader.Read())
                        {
                            username = dataReader["Email"].ToString();
                            pass = dataReader["Password"].ToString();
                            if (pass == password.Trim(charsToRemove))
                            {
                                rCLientID = dataReader["CLientID"].ToString();
                                rFirstn = dataReader["Name"].ToString();
                                rSurname = dataReader["Surname"].ToString();
                                rDOB = dataReader["Date_Of_Birth"].ToString();
                                rGender = dataReader["Gender"].ToString();
                                rStreet = dataReader["Street_Address"].ToString();
                                rCity = dataReader["City"].ToString();
                                rProvince = dataReader["Province"].ToString();
                                rZip = dataReader["Postal_Code"].ToString();
                                rEmail = dataReader["Email"].ToString();
                                rPassword = dataReader["Password"].ToString();
                                con.Close();
                                return Request.CreateResponse(HttpStatusCode.Accepted, "Login Successful");
                            }
                            else
                            {
                                con.Close();
                                return Request.CreateResponse(HttpStatusCode.NoContent, "The password is wrong");

                            }

                        }
                        else
                        {
                            con.Close();
                            return Request.CreateResponse(HttpStatusCode.NoContent, "The password is wrong");

                        }



                    }
                    else
                    {
                        con.Close();
                        return Request.CreateResponse(HttpStatusCode.NotFound, "The Client Profile does not exits");

                    }
                }
                else
                {
                    con.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, "Database not connected");

                }
            }
            catch (Exception ex)
            {

                con.Close();
                return Request.CreateResponse(HttpStatusCode.OK, $"Error{ex.Message}");

            }
        }
    }
}
