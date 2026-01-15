using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SMSDAL
/// </summary>
namespace SMSBAL
{
    public class SMS
    {
        public bool success { get; set; }
        public string message { get; set; }
    }
    public class Data
    {
        public string user_name { get; set; }
        public decimal total_balance { get; set; }
        public int total_credit { get; set; }
        public string job_id { get; set; }
        public string mobile { get; set; }
        public string status { get; set; }
        public string message { get; set; }
    }
    public class SMSReport : SMS
    {
        public List<Data> data { get; set; }
    }
    public class SMSBalance : SMS
    {
        public bool success { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
    }
}