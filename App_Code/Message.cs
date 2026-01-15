using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

/// <summary>
/// Summary description for Message
/// </summary>
public class Message:BAL.textBoxList
{
    Campus oo = new Campus();
  
    public string MessageType(string messagetype,Control parentctrl,BAL.textBoxList nonCleartxtlist)
    {
        string message="";
        switch (messagetype)
        {
            case "D":
                message= "Sorry, Duplicate Record(s) Found!";
                break;
            case "S":
                message = "Submitted successfully.";
                 int count = nonCleartxtlist.Noofnoncleartxt;
                 switch (count)
                 {
                     case 1:
                         {
                             oo.ClearControls(parentctrl, nonCleartxtlist.txt1);
                         }
                         break;
                     case 2:
                         {
                             oo.ClearControls(parentctrl, nonCleartxtlist.txt1, nonCleartxtlist.txt2);
                         }
                         break;
                     case 3:
                         {
                             oo.ClearControls(parentctrl, nonCleartxtlist.txt1, nonCleartxtlist.txt2, nonCleartxtlist.txt3);
                         }
                         break;
                     case 4:
                         {
                             oo.ClearControls(parentctrl, nonCleartxtlist.txt1, nonCleartxtlist.txt2, nonCleartxtlist.txt3, nonCleartxtlist.txt4);
                         }
                         break;
                     case 5:
                         {
                             oo.ClearControls(parentctrl, nonCleartxtlist.txt1, nonCleartxtlist.txt2, nonCleartxtlist.txt3, nonCleartxtlist.txt4, nonCleartxtlist.txt5);
                         }
                         break;
                     default:
                         {
                             oo.ClearControls(parentctrl);
                         }
                         break;
                  }
                 break;
            case "N":
                 message= "Sorry, No Record(s) found!";
                break;
            case "E":
                message = "Sorry, Please enter details!";
                break;
            case "U":
                message = "Updated successfully.";
                break;
            case "De":
                message = "Deleted successfully.";
                break;
            case "NU":
                message = "Sorry, Record(s) not Update!";
                break;
            default:
                message = messagetype;
                break;
        }    
        return message;
    }

    public string SimpleMessageType(string messagetype, Control parentctrl)
    {
        string message = "";
        switch (messagetype)
        {
            case "D":
                message = "Sorry, Duplicate Record(s) Found!";
                break;
            case "S":
                message = "Submitted successfully.";
                oo.ClearControls(parentctrl);                   
                break;
            case "N":
                message = "Sorry, No Record(s) found!";
                break;
            case "E":
                message = "Sorry, Please enter details!";
                break;
            case "U":
                message = "Updated successfully.";
                break;
            case "De":
                message = "Deleted successfully.";
                break;
            default:
                message = messagetype;
                break;
        }
        return message;
    }
}