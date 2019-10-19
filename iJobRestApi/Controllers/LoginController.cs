using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace iJobRestApi.Controllers
{
    public class LoginController : ApiController
    {
        private static string your_username = "ijob", your_password = "Tanya@dodger12";
        private SqlConnection con = new SqlConnection($"Server=tcp:ijobsrvr.database.windows.net,1433;Initial Catalog=iJobDatabase;Persist Security Info=False;User ID={your_username};Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=50;");

       
   
        public ClientObject GetClientObject(string Email,string Password)
        {
            try
            {
                con.Open();
                if (con.State == System.Data.ConnectionState.Open)
                {
                    char[] charsToRemove = { '{', '}' };
                    string email = Email.Trim(charsToRemove);

                    string Query = $"SELECT * FROM Client where Email='{email}'";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    string username = "", pass = "";
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        if (dataReader.Read())
                        {
                            ClientObject client = new ClientObject();
                            username = dataReader["Email"].ToString();
                            pass = dataReader["Password"].ToString();
                            if (pass == Password.Trim(charsToRemove))
                            {
                            client.ClientID = dataReader["CLientID"].ToString();
                            client.FirsName         = dataReader["Name"].ToString();
                            client.Surname          = dataReader["Surname"].ToString();
                            client.DOB              = dataReader["Date_Of_Birth"].ToString();
                            client.Gender           = dataReader["Gender"].ToString();
                            client.StreetAddress    = dataReader["Street_Address"].ToString();
                            client.City             = dataReader["City"].ToString();
                            client.Province         = dataReader["Province"].ToString();
                            client.ZipCode          = int.Parse(dataReader["Postal_Code"].ToString());
                            client.Email            = dataReader["Email"].ToString();
                            client.Password         = dataReader["Password"].ToString();
                                con.Close();
                                return client;
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
            catch (Exception ex)
            {

                con.Close();
                return null;

            }
        }



    }
}
