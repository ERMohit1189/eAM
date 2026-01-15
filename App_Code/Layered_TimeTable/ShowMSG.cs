using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ShowMSG
/// </summary>
public class ShowMSG
{
    public string MSG(string MSGCode)
    {
        string MSG = "";
        switch (MSGCode)
        {
            case "I":
                {
                    MSG = "Submitted successfully.";
                    break;
                }
            case "U":
                {
                    MSG = "Updated successfully.";
                    break;
                }
            case "D":
                {
                    MSG = "Deleted successfully.";
                    break;
                }
            case "E":
                {
                    MSG = "Record Already Exists !";
                    break;
                }
            case "N":
                {
                    MSG = "No Record Found !";
                    break;
                }
            default:
                {
                    MSG = MSGCode;
                    break;
                }
        }
        return MSG;
    }
}