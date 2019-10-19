using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iJobRestApi
{
    public class JobRequests
    {
        public string   ClientID        { get; set; }
        public string   Date            { get; set; }
        public string   TimeStart       { get; set; }
        public string   TimeEnd          { get; set; }
        public string   Category            { get; set; }
        public int      NumOfLabs           { get; set; }
        public string   JobSpec         { get; set; }
        public string   Location        { get; set; }
        public bool     TranspotStatus { get; set; }
        public bool     IsAssigned      { get; set; }
        public decimal  Cost            { get; set; }
    }
}