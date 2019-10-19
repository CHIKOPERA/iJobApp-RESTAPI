using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iJobRestApi
{
    public class ClientObject
    {
        public string ClientID      { get; set; }
        public string FirsName       { get; set; }
        public string Surname       { get; set; }
        public string DOB            { get; set; }
        public string Gender             { get; set; }
        public string StreetAddress     { get; set; }
        public string City              { get; set; }
        public string Province           { get; set; }
        public int    ZipCode        { get; set; }
        public string Email         { get; set; }
        public string Password       { get; set; }
    }
}