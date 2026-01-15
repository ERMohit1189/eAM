using c4SmsNew;
using System;

/// <summary>
/// Summary description for SMS
/// </summary>
public class SMS
{
    private readonly Campus oo = new Campus();
    string sql = "";
    public string SendSms(string contactNo, string msg, string smsPageId, string branchCode)
    {
        string res = "0";
        try
        {
            sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + branchCode + "";
            if (oo.ReturnTag(sql, "HitValue") == "") return res;
            if (oo.ReturnTag(sql, "HitValue") != "true") return res;
            string sql1 = "Select SmsSent From SmsEmailMaster Where Id='" + smsPageId + "' and BranchCode=" + branchCode + "";
            if (oo.ReturnTag(sql1, "SmsSent").Trim() == "true")
            {
                var sadpNew = new SMSAdapterNew();
                if (contactNo == "") return "0";
                sadpNew.Send(msg, contactNo, smsPageId);
                res = "1";
            }
        }
        catch (Exception ex)
        {
            // ignored
        }

        return res;
    }

}